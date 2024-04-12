using Mango.Services.AuthApi.Data;
using Mango.Services.AuthApi.Models;
using Mango.Services.AuthApi.Models.Dto_s;
using Mango.Services.AuthApi.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext dbContext, 
            UserManager<ApplicationUser> userMnager, RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _userManager = userMnager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(U => U.NormalizedEmail == email.ToUpper());
            if(user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginDto)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.NormalizedUserName == loginDto.UserName.ToUpper());    
            
            bool isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        
            if(user==null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            var roles = await _userManager.GetRolesAsync(user);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = _jwtTokenGenerator.GenerateToken(user, roles)
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegisterRequestDto registerDto)
        {
            ApplicationUser user = new()
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                NormalizedEmail = registerDto.Email.ToUpper(),
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    //var userToReturn = _dbContext.ApplicationUsers.First(u => u.UserName == registerDto.Email);

                    //UserDto userDto = new()
                    //{
                    //    Email = user.Email,
                    //    ID = user.Id,
                    //    Name = user.Name,
                    //    PhoneNumber = user.PhoneNumber,
                    //};

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            } catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
