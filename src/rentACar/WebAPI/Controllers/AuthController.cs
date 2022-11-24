using Application.Features.Auths.Commands.EnableEmailAuthenticator;
using Application.Features.Auths.Commands.EnableOtpAuthenticator;
using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.RefleshToken;
using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Commands.RevokeToken;
using Application.Features.Auths.Commands.VerifyEmailAuthenticator;
using Application.Features.Auths.Commands.VerifyOtpAuthenticator;
using Application.Features.Auths.Dtos;
using Core.Application.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly WebAPIConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("WebAPIConfiguration").Get<WebAPIConfiguration>();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IPAddress = getIpAddress() };
        LoggedDto result = await Mediator.Send(loginCommand);

        if (result.RefreshToken is not null) setRefreshTokenToCookie(result.RefreshToken);

        return Ok(result.CreateResponseDto());
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, IPAddress = getIpAddress() };
        RegisteredDto result = await Mediator.Send(registerCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created("", result.AccessToken);
    }

    [HttpGet("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenCommand refreshTokenCommand = new()
            { RefleshToken = getRefreshTokenFromCookies(), IPAddress = getIpAddress() };
        RefreshedTokensDto result = await Mediator.Send(refreshTokenCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created("", result.AccessToken);
    }

    [HttpPut("RevokeToken")]
    public async Task<IActionResult> RevokeToken(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]
        string? refreshToken)
    {
        RevokeTokenCommand revokeTokenCommand = new()
        {
            Token = refreshToken ?? getRefreshTokenFromCookies(),
            IPAddress = getIpAddress()
        };
        RevokedTokenDto result = await Mediator.Send(revokeTokenCommand);
        return Ok(result);
    }

    [HttpGet("EnableEmailAuthenticator")]
    public async Task<IActionResult> EnableEmailAuthenticator()
    {
        EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand = new()
        {
            UserId = getUserIdFromRequest(),
            VerifyEmailUrlPrefix = $"{_configuration.APIDomain}/Auth/VerifyEmailAuthenticator"
        };
        await Mediator.Send(enableEmailAuthenticatorCommand);

        return Ok();
    }

    [HttpGet("EnableOtpAuthenticator")]
    public async Task<IActionResult> EnableOtpAuthenticator()
    {
        EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand = new()
        {
            UserId = getUserIdFromRequest()
        };
        EnabledOtpAuthenticatorDto result = await Mediator.Send(enableOtpAuthenticatorCommand);

        return Ok(result);
    }

    [HttpGet("VerifyEmailAuthenticator")]
    public async Task<IActionResult> VerifyEmailAuthenticator(
        [FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
    {
        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    [HttpPost("VerifyOtpAuthenticator")]
    public async Task<IActionResult> VerifyOtpAuthenticator(
        [FromBody] string authenticatorCode)
    {
        VerifyOtpAuthenticatorCommand verifyEmailAuthenticatorCommand =
            new() { UserId = getUserIdFromRequest(), ActivationCode = authenticatorCode };

        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    private string? getRefreshTokenFromCookies()
    {
        return Request.Cookies["refreshToken"];
    }

    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}