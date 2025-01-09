using Mango.Web.Models;
using Mango.Web.Models.Auth;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class AuthService : IAuthService
    {
        private const string authApi = "/api/auth";

        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> AssignRoleAsync(AssignRoleRequestDto assignRoleRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.AuthAPIBase}{authApi}/assignRole",
                Data = assignRoleRequestDto
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.AuthAPIBase}{authApi}/login",
                Data = loginRequestDto
            }, withBearer: false);
        }

        public async Task<ResponseDto> RegisterAsync(RegisterationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.AuthAPIBase}{authApi}/register",
                Data = registerationRequestDto
            }, withBearer: false);
        }
    }
}
