using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Auth.Commands;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace OnlineCoursesPlatform.API.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var response = await _mediator.Send(new LoginUser(dto));
        return Ok(response);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var response = await _mediator.Send(new RegisterUser(dto));
        return Ok(response);
    }

    [HttpGet]
    [Route("protected")]
    [Authorize]
    public IActionResult ProtectedRoute() => Ok("You are authorized");

    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/login");
    }

    [HttpGet]
    [Route("login/google")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleCallback", "Auth")!
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("signin-google")]
    public async Task<IActionResult> GoogleCallback([FromServices] IMediator mediator)
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;

        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(email))
            return Problem("Google did not return an email");

        var dto = new CreateUserFromGoogleDto { Email = email, UserName = name ?? email };
        var authResult = await mediator.Send(new CreateUserFromGoogle(dto));

        return Ok(authResult);
    }
}
