using BudgetZ.API.Data;
using BudgetZ.API.Models;
using BudgetZ.API.Models.Response;
using BudgetZ.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetZ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public UsersController(ApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<UserRegisterResponse>>> Register([FromBody] RegisterDto request)
    {
        // Username boş kontrolü
        if (string.IsNullOrEmpty(request.Username))
        {
            return BadRequest(new ApiResponse<UserRegisterResponse>
            {
                Success = false,
                Message = "Kullanıcı adı boş olamaz.",
                Data = null
            });
        }

        // Username uzunluk kontrolü
        if (request.Username.Length < 3 || request.Username.Length > 20)
        {
            return BadRequest(new ApiResponse<UserRegisterResponse>
            {
                Success = false,
                Message = "Kullanıcı adı 3 ile 20 karakter arasında olmalıdır.",
                Data = null
            });
        }

        // Password boş kontrolü
        if (string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new ApiResponse<UserRegisterResponse>
            {
                Success = false,
                Message = "Şifre boş olamaz.",
                Data = null
            });
        }

        // Password uzunluk kontrolü
        if (request.Password.Length < 8 || request.Password.Length > 16)
        {
            return BadRequest(new ApiResponse<UserRegisterResponse>
            {
                Success = false,
                Message = "Şifre 8 ile 16 karakter arasında olmalıdır.",
                Data = null
            });
        }

        // Username unique kontrolü
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (existingUser != null)
        {
            return BadRequest(new ApiResponse<UserRegisterResponse>
            {
                Success = false,
                Message = "Bu kullanıcı adı zaten kullanımda.",
                Data = null
            });
        }

        // Yeni kullanıcı oluştur
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            IsAdmin = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Response oluştur
        var response = new UserRegisterResponse
        {
            Id = user.Id,
            Username = user.Username,
            IsAdmin = user.IsAdmin
        };

        return StatusCode(StatusCodes.Status201Created, new ApiResponse<UserRegisterResponse>
        {
            Success = true,
            Message = "Kullanıcı başarıyla oluşturuldu.",
            Data = response
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<UserLoginResponse>>> Login([FromBody] LoginDto request)
    {
        // Username boş kontrolü
        if (string.IsNullOrEmpty(request.Username))
        {
            return BadRequest(new ApiResponse<UserLoginResponse>
            {
                Success = false,
                Message = "Kullanıcı adı boş olamaz.",
                Data = null
            });
        }

        // Password boş kontrolü
        if (string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new ApiResponse<UserLoginResponse>
            {
                Success = false,
                Message = "Şifre boş olamaz.",
                Data = null
            });
        }

        // Kullanıcıyı bul
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null)
        {
            return BadRequest(new ApiResponse<UserLoginResponse>
            {
                Success = false,
                Message = "Bu kullanıcı adı sistemde bulunamadı.",
                Data = null
            });
        }

        // Şifre kontrolü
        if (user.Password != request.Password)
        {
            return BadRequest(new ApiResponse<UserLoginResponse>
            {
                Success = false,
                Message = "Şifre hatalı.",
                Data = null
            });
        }

        // Token oluştur
        var token = _tokenService.CreateToken(user);

        // Response oluştur
        var response = new UserLoginResponse
        {
            Id = user.Id,
            Username = user.Username,
            IsAdmin = user.IsAdmin,
            Token = token
        };

        return Ok(new ApiResponse<UserLoginResponse>
        {
            Success = true,
            Message = "Giriş başarılı.",
            Data = response
        });
    }
}

public class RegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
} 