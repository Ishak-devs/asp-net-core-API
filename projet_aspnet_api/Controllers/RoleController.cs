using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
using projet_aspnet_api;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly Context _context;

    public RoleController(Context Context)
    {
        _context = Context;
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Nom == username && u.Password == password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        // Création d'un objet claims
        var Myclaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("Role", user.role)
        };

        if (user.role != "admin")
        {
            return Unauthorized(new { message = "Vous n'êtes pas autorisé à accéder" });
        }

        // Identité claim regroupant l'ensemble des informations
        var claimsIdentity = new ClaimsIdentity(Myclaims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Création d'un cookie du schéma d'authentification
        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        // Retourne un message connexion réussie
        return Ok(new { message = $"{user.role} logged in successfully", role = user.role });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        // Effacer le cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Logged out successfully");
    }
}
