using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace projet_aspnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Loginmodele loginUser)
        {
            var user = _context.Users.SingleOrDefault(u => u.Nom == loginUser.Nom && u.Password == loginUser.Password);
            if (user == null)
            {
                return Unauthorized("Nom ou mot de passe invalide");
            }
            return Ok("Connexion réussie");
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé");
            }
            return Ok(user);
        }
    }
}
