using API.Dtos;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return Unauthorized();
        var result = _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (result.IsCompletedSuccessfully)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto dto)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email))
        {
            return BadRequest("Email Token");
        }

        if (await _userManager.Users.AnyAsync(x => x.UserName == dto.Username))
        {
            return BadRequest("Username taken");
        }

        var user = new AppUser()
        {
            DisplayName = dto.DisplayName,
            Bio = dto.Bio,
            Email = dto.Email,
            UserName = dto.Username
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (result.Succeeded)
        {
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }

        return BadRequest("Problem register");
    }
}