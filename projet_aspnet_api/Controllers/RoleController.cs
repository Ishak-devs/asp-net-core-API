using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public RoleController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    [Authorize("admin")]
    public IActionResult Login([FromBody] User request)
    {
        // Simuler une vérification d'utilisateur
        if (request.Nom == "admin" && request.Password == "password")
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, request.Nom),
            new Claim(ClaimTypes.Role, "Admin") // Rôle de l'utilisateur
        };
            return Ok("ok");

        }
        return Unauthorized("acces refusé");
    }
}