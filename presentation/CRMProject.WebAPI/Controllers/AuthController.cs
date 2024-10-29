using CRMProject.Application.Dtos;
using CRMProject.Domain.Entities;
using CRMProject.Persistance.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null || !IsPasswordValid(model.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Unauthorized("Email və ya şifrə yalnışdır.");
        }

        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (await IsEmailInUse(model.Email))
        {
            return Conflict("Bu email artıq istifadə olunur.");
        }

        var user = CreateUser(model);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok("Qeydiyyat uğurlu oldu.");
    }

    private async Task<bool> IsEmailInUse(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    private User CreateUser(RegisterModel model)
    {
        using (var hmac = new HMACSHA256())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(model.Password);
            var salt = hmac.Key; 
            var hash = hmac.ComputeHash(passwordBytes);

            return new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = Convert.ToBase64String(hash),
                PasswordSalt = Convert.ToBase64String(salt), 
                Role = "User"
            };
        }
    }

    private bool IsPasswordValid(string password, string storedPasswordHash, string storedPasswordSalt)
    {
        using (var hmac = new HMACSHA256(Convert.FromBase64String(storedPasswordSalt)))
        {
            var hashedInputPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedInputPassword) == storedPasswordHash;
        }
    }
}
