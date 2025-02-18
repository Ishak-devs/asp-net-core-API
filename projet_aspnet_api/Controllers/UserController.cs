using Microsoft.AspNetCore.Mvc;
using System.Linq; 

namespace projet_aspnet_api.Controllers
{
    [ApiController] //indication que le controlleur est un controlleur API
    public class UsersController : ControllerBase
    {
        private readonly Context _context; //on recuperer le contexte

        public UsersController(Context context)
        {
            _context = context; //stockage du contexte dans une variable
        }

        [HttpPost("Connexion")]
        public IActionResult Login([FromBody] Loginmodele loginUser)
        {
            var user = _context.Users.SingleOrDefault(u => u.Nom == loginUser.Nom && u.Password == loginUser.Password);
            if (user == null)
            {
                return Unauthorized("Nom ou mot de passe invalide");
            }
            return Ok("Connexion réussie");
        }

        [HttpPost("Modifier un utilisateur")]
        public IActionResult Edit([FromBody] Users updatedUser)
        {
            var user = _context.Users.SingleOrDefault(u => u.Nom == updatedUser.Nom);
            if (user == null)
            {
                return Unauthorized("Utilisateur introuvable");
            }

            user.Nom = updatedUser.Nom;
            user.Password = updatedUser.Password;

            _context.SaveChanges();
            return Ok("Modification réussie");
        }

        [HttpPut("AddUser")]
        public IActionResult Put([FromBody] Users newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{nom}")]
        public IActionResult GetUser(string nom)
        {
            var user = _context.Users.SingleOrDefault(u => u.Nom == nom);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé");
            }
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{nom}")]
        public IActionResult Delete(string nom)
        {
            var user = _context.Users.SingleOrDefault(u => u.Nom == nom);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok("Utilisateur supprimé avec succès");
        }
    }
}
