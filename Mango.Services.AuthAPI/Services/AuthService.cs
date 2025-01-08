using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email!.ToLower() == email.ToLower());

            if (user == null)
            {
                return false;
            }

            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);

            return true;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

            if (user == null)
            {
                return new LoginResponseDto();
            }

            bool isValid = await _userManager.CheckPasswordAsync(user!, loginRequestDto.Password);

            if (isValid == false)
            {
                return new LoginResponseDto();
            }

            var token = _jwtTokenGenerator.GenerateToken(user!);

            UserDto userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };

            return new LoginResponseDto
            {
                User = userDto,
                Token = token,
            };
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registerationRequestDto.Email,
                NormalizedUserName = registerationRequestDto.Email.ToUpper(),
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                Name = registerationRequestDto.Name,
                PhoneNumber = registerationRequestDto.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user: user, password: registerationRequestDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName!.ToLower() == registerationRequestDto.Email.ToLower()) ?? throw new Exception("Unexpected error");

                    UserDto userDto = new()
                    {
                        Email = userToReturn!.Email!,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn!.PhoneNumber!,
                    };

                    return "";
                }
                else
                {
                    return result!.Errors!.FirstOrDefault()!.Description;
                }
            }
            catch (Exception ex)
            {
            }

            return "Error Encountered";
        }
    }
}
