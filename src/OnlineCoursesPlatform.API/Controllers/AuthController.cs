using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Application.Features.Auth.Commands;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;

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

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        var response = await _mediator.Send(new RegisterUser(dto));
        return Ok(response);
    }

    [HttpPost("set-password")]
    public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto dto)
    {
        await _mediator.Send(new SetPasswordCommand(dto));
        return Ok(new { Message = "Password successfully set." });
    }

    [HttpPost("request-new-link")]
    public async Task<IActionResult> RequestNewLink([FromBody] RequestNewLinkCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { Message = "New password link sent to your email." });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { Message = "Password reset link sent to your email." });
    }

    //[HttpPost]
    //[Route("register-teacher")]
    //public async Task<IActionResult> RegisterTeacher([FromBody] CreateTeacherRegistrationRequestDto dto)
    //{
    //    await _mediator.Send(new RegisterTeacher(dto));
    //    return Ok(new { Message = "Teacher registration request submitted successfully. Please wait for approval." });
    //}

    //[HttpPost("approve-teacher/{teacherId}")]
    //public async Task<IActionResult> ApproveTeacher(int teacherId)
    //{
    //    await _mediator.Send(new ApproveTeacher(teacherId));
    //    return Ok("Teacher approved and password setup email sent.");
    //}

    //[HttpPost]
    //[Route("set-password")]
    //public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto dto)
    //{
    //    var response = await _mediator.Send(new SetPassword(dto));
    //    return Ok(response);
    //}

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
    {
        var result = await _mediator.Send(new RefreshTokenCommand(dto));
        return Ok(result);
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

    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { Message = "You have been logged out." });
    }

    [HttpGet]
    [Route("protected")]
    [Authorize]
    public IActionResult ProtectedRoute() => Ok("You are authorized");

}
