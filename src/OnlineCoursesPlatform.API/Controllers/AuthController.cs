using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Auth.Commands;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using System.Security.Claims;

namespace OnlineCoursesPlatform.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _mediator.Send(new LoginUser(dto));

            // Установка HttpOnly refresh-токена
            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = response.RefreshTokenExpiration,
                IsEssential = true
            });

            return Ok(new
            {
                accessToken = response.AccessToken,
                accessTokenExpiration = response.AccessTokenExpiration,
                userInfo = response.UserInfo
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "Missing refresh token." });
            }

            var result = await _mediator.Send(new RefreshTokenCommand(new RefreshTokenRequestDto
            {
                RefreshToken = refreshToken
            }));

            // Обновление HttpOnly refresh-токена
            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.RefreshTokenExpiration,
                IsEssential = true
            });

            return Ok(new
            {
                accessToken = result.AccessToken,
                accessTokenExpiration = result.AccessTokenExpiration,
                userInfo = result.UserInfo
            });
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("refreshToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "You have been logged out." });
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
        {
            var response = await _mediator.Send(new RegisterUser(dto));

            // Устанавливаем refreshToken в HttpOnly cookie
            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = response.RefreshTokenExpiration,
                IsEssential = true
            });

            return Ok(new
            {
                accessToken = response.AccessToken,
                accessTokenExpiration = response.AccessTokenExpiration,
                userInfo = response.UserInfo
            });
        }


        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto dto)
        {
            await _mediator.Send(new SetPasswordCommand(dto));
            return Ok(new { message = "Password successfully set." });
        }

        [HttpPost("request-new-link")]
        public async Task<IActionResult> RequestNewLink([FromBody] RequestNewLinkCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "New password link sent to your email." });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Password reset link sent to your email." });
        }

        [HttpGet("login/google")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback", "Auth")!
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;

            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var avatar = claims?.FirstOrDefault(c => c.Type == "picture")?.Value;

            if (string.IsNullOrEmpty(email))
                return Problem("Google did not return an email");

            var dto = new CreateUserFromGoogleDto
            {
                Email = email,
                UserName = name ?? email,
                AvatarUrl = avatar
            };

            var authResult = await _mediator.Send(new CreateUserFromGoogle(dto));

            // Устанавливаем refresh token в cookie
            Response.Cookies.Append("refreshToken", authResult.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = authResult.RefreshTokenExpiration,
                IsEssential = true
            });

            // Перенаправляем на фронт с accessToken в query
            var frontendUrl = $"https://localhost:5173/google-callback" +
                  $"?accessToken={Uri.EscapeDataString(authResult.AccessToken)}" +
                  $"&accessTokenExpiration={Uri.EscapeDataString(authResult.AccessTokenExpiration.ToString("o"))}" +
                  $"&userName={Uri.EscapeDataString(authResult.UserInfo.UserName)}" +
                  $"&avatarUrl={Uri.EscapeDataString(authResult.UserInfo.AvatarUrl ?? "")}";


            return Redirect(frontendUrl);
        }


        [HttpGet("protected")]
        [Authorize]
        public IActionResult ProtectedRoute()
        {
            return Ok("You are authorized");
        }
    }
}
