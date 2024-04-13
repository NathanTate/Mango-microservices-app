using AutoMapper;
using Mango.Services.ProductAPI;
using Mango.Services.ShoopingCartAPI.Data;
using Mango.Services.ShoopingCartAPI.Extensions;
using Mango.Services.ShoopingCartAPI.Service;
using Mango.Services.ShoopingCartAPI.Service.IService;
using Mango.Services.ShoopingCartAPI.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<BackendApiAuthHttpClientHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient("Product", u => u.BaseAddress =
    new Uri(builder.Configuration["ServiceUrls:ProductAPI"]))
        .AddHttpMessageHandler<BackendApiAuthHttpClientHandler>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddHttpClient("Coupon", u => u.BaseAddress =
    new Uri(builder.Configuration["ServiceUrls:CouponAPI"]))
        .AddHttpMessageHandler<BackendApiAuthHttpClientHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },  new string[] { }
        }
    });
});
builder.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigrations();
app.Run();

void ApplyMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
