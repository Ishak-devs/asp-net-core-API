using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projet_aspnet_api.Controllers
{
    public class AccesController : Controller
    {
        [HttpGet("privé")]
        [Authorize(Roles = "Admin")] // Seuls les utilisateurs avec le rôle "Admin" peuvent accéder
        public IActionResult GetAdminData()
        {
           
            return Ok(new { message = "Ceci est une route protégée pour les administrateurs." });
        }


        [HttpGet("public")]
        [AllowAnonymous] // Cette route est accessible sans authentification
        public IActionResult GetPublicData()
        {
            return Ok(new { message = "Ceci est une route publique." });
        }
    }
}