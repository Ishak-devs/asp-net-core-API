using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class usersessionController : Controller
{
    [HttpGet("set")]
    public IActionResult SetSession(string valeurSession)
    {
        HttpContext.Session.SetString("MaSession", valeurSession);
        return Content("Session ajoutée !");
    }

    [HttpGet("get")]
    public IActionResult GetSession()
    {
        var valeurDeSession = HttpContext.Session.GetString("MaSession");

        if (valeurDeSession != null)
        {
            return Content($"Valeur de la session : {valeurDeSession}");
        }
        else
        {
            return Content("Session non trouvée.");
        }
    }

    [HttpDelete("delete")]
    public IActionResult DeleteSession()
    {
        HttpContext.Session.Remove("MaSession");
        return Content("Session supprimée avec succès");
    }
}