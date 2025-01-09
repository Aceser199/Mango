using Mango.Web.Models;
using Mango.Web.Models.Auth;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            if (ModelState.IsValid)
            {
                ResponseDto responseDto = await _authService.LoginAsync(obj);

                if (responseDto != null && responseDto.IsSuccess)
                {
                    LoginResponseDto? loginResponseDto =
                        JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                    await SignInUser(loginResponseDto!);
                    _tokenProvider.SetToken(loginResponseDto!.Token);
                    TempData["success"] = "You were sign in successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", responseDto!.Message);
                    TempData["error"] = responseDto?.Message;
                }
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Register()
        {
            //var roleList = new List<SelectListItem>()
            //{
            //    new SelectListItem{ Text = SD.RoleAdmin, Value = SD.RoleAdmin },
            //    new SelectListItem{ Text = SD.RoleCustomer, Value = SD.RoleCustomer },
            //};

            //ViewBag.RoleList = roleList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequestDto obj)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _authService.RegisterAsync(obj);

                if (response != null && response.IsSuccess)
                {
                    AssignRoleRequestDto assignRoleRequestDto = new()
                    {
                        Email = obj.Email,
                        RoleName = SD.RoleCustomer,
                    };
                    ResponseDto assingRoleResponse = await _authService.AssignRoleAsync(assignRoleRequestDto);
                    if (assingRoleResponse != null && assingRoleResponse.IsSuccess)
                    {
                        TempData["success"] = "You were register successfully. Please sign in.";

                        return RedirectToAction(nameof(Login));
                    }
                }
                else
                {
                    TempData["error"] = response?.Message;
                }

            }

            return View(obj);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            TempData["success"] = "You were sign out successfully";
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt!.Claims!.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
