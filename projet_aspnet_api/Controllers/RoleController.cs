using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using projet_aspnet_api;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")] 
public class RoleController : ControllerBase
{
    private readonly Context _context;

    public RoleController(Context context)
    {
        _context = context;
    }

    [AllowAnonymous] 
    [HttpPost("login")]
    [Authorize(Roles = "client")]
    public IActionResult Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Nom == username && u.Password == password);
        if (user == null)
        {
            return Unauthorized(new { message = "Vous n'avez pas le role permettant l'accès" });
        }

        // Création d'un objet claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Nom),
            new Claim(ClaimTypes.Role, user.role) // Ajout du rôle dans les claims
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        return Ok(new { message = $"{user.role} logged in successfully", role = user.role });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Logged out successfully");
    }
}
