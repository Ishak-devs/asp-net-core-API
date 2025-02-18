//using Microsoft.AspNetCore.Mvc;
//using System.Linq; // On importe les outils nécessaires pour manipuler les requêtes LINQ

//namespace projet_aspnet_api.Controllers
//{
//    // On dit à ASP.NET que cette classe gère les requêtes API
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        // On crée une variable pour stocker le contexte de la base de données
//        private readonly Context _context;

//        // Le constructeur du contrôleur, il initialise le contexte de la base de données
//        public UsersController(Context context)
//        {
//            _context = context; // On assigne le contexte de la base de données à notre variable privée
//        }

//        // On définit un endpoint POST pour la connexion des utilisateurs
//        [HttpPost("Connexion")]
//        public IActionResult Login([FromBody] Loginmodele loginUser)
//        {
//            // On cherche l'utilisateur dans la base de données avec le même nom et mot de passe
//            var user = _context.Users.SingleOrDefault(u => u.Nom == loginUser.Nom && u.Password == loginUser.Password);
//            // Si l'utilisateur n'existe pas, on renvoie un statut non autorisé
//            if (user == null)
//            {
//                return Unauthorized("Nom ou mot de passe invalide");
//            }
//            // Si tout va bien, on renvoie un message de succès
//            return Ok("Connexion réussie");
//        }

//        // On définit un endpoint PUT pour insérer un nouvel utilisateur
//        [HttpPut("AddUser")]
//        public IActionResult Put([FromBody] Users newUser)
//        {
//            _context.Users.Add(newUser);
//            _context.SaveChanges();
//            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
//        }

//        // Ce code est commenté, mais il servirait à récupérer tous les utilisateurs
//        //[HttpGet("Users")]
//        //public IActionResult GetUser(int id)
//        //{
//        //    var user = _context.Users;
//        //    if (user == null)
//        //    {
//        //        return NotFound("Utilisateur non trouvé");
//        //    }
//        //    return Ok(user);
//        //}
//    }
//}




using Microsoft.AspNetCore.Mvc;
using System.Linq; // On importe les outils nécessaires pour manipuler les requêtes LINQ

namespace projet_aspnet_api.Controllers
{
    // On dit à ASP.NET que cette classe gère les requêtes API
    [ApiController]
    public class UsersController : ControllerBase
    {
        // On crée une variable pour stocker le contexte de la base de données
        private readonly Context _context;

        // Le constructeur du contrôleur, il initialise le contexte de la base de données
        public UsersController(Context context)
        {
            _context = context; // On assigne le contexte de la base de données à notre variable privée
        }

        // On définit un endpoint POST pour la connexion des utilisateurs
        [HttpPost("Connexion")]
        public IActionResult Login([FromBody] Loginmodele loginUser)
        {
            // On cherche l'utilisateur dans la base de données avec le même nom et mot de passe
            var user = _context.Users.SingleOrDefault(u => u.Nom == loginUser.Nom && u.Password == loginUser.Password);
            // Si l'utilisateur n'existe pas, on renvoie un statut non autorisé
            if (user == null)
            {
                return Unauthorized("Nom ou mot de passe invalide");
            }
            // Si tout va bien, on renvoie un message de succès
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
                    user.Password = updatedUser.Password; // Assurez-vous de mettre à jour les autres informations nécessaires

                    _context.SaveChanges();
                    return Ok("Modification réussie");
}




        // On définit un endpoint PUT pour insérer un nouvel utilisateur
        [HttpPut("AddUser")]
        public IActionResult Put([FromBody] Users newUser)
        {
            // On ajoute le nouvel utilisateur dans la base de données
            _context.Users.Add(newUser);
            // On sauvegarde les changements
            _context.SaveChanges();
            // On renvoie un message de succès avec le statut 201 (Created)
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // On définit un endpoint GET pour récupérer un utilisateur par ID
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
            // On cherche l'utilisateur dans la base de données par son ID
            var user = _context.Users.SingleOrDefault(u => u.Nom == nom);
            // Si l'utilisateur n'existe pas, on renvoie un statut Not Found
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé");
            }
            // On supprime l'utilisateur de la base de données
            _context.Users.Remove(user);
            // On sauvegarde les changements
            _context.SaveChanges();
            // On renvoie un message de succès
            return Ok("Utilisateur supprimé avec succès");
        }




    }
}
