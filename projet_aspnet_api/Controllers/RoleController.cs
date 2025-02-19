using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
using projet_aspnet_api;

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
        if (user != null)
        {
            //Création d'un objet claims
            var Myclaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("Role", user.role)
            };

            //Identité claim regroupant l'ensemble des informations
            var claimsIdentity = new ClaimsIdentity(Myclaims, CookieAuthenticationDefaults.AuthenticationScheme);

            //Création d'un cookie du shéma d'authentification
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


            //Retourne un message connexion réussie
            return Ok(new { message = $"{user.role} logged in successfully", role = user.role });
            
        }
        
        //Retourne un message accès refusé
        return Unauthorized("Invalid credentials");
    }

 
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        //Effacer le cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Logged out successfully");
    }
}
