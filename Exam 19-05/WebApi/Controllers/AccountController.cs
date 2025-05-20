using Domain.Constants;
using Domain.DTOs.Auth;
using Domain.DTOs.User;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<Response<string>> Register(RegisterDto registerDto)
    {
        return await authService.Register(registerDto);
    }

    [HttpPost("login")]
    public async Task<Response<TokenDto>> Login(LoginDto loginDto)
    {
        return await authService.Login(loginDto);
    }

    [HttpPost("reset-password")]
    public async Task<Response<string>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        return await authService.ResetPassword(resetPasswordDto);
    }

    [HttpPost("request-reset-password")]
    public async Task<Response<string>> RequestResetPassword(RequestResetPassword resetPasswordDto)
    {
        return await authService.RequestResetPassword(resetPasswordDto);
    }  
    
    [HttpPost("change-role")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<Response<string>> ChangeUserRole(RoleChangerDto dto)
    {
        return await authService.ChangeUserRoleAsync(dto);
    }
}