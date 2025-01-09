using Mango.Web.Models;
using Mango.Web.Models.Auth;

namespace Mango.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(RegisterationRequestDto registerationRequestDto);
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRoleAsync(AssignRoleRequestDto assignRoleRequestDto);
    }
}
