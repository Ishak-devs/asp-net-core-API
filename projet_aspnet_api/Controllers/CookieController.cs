//using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Http;

//[ApiController]
//public class CookieController : Controller
//{
//    [HttpGet("set")]
//    public IActionResult SetCookie(string valeurcookie)
//    {

//        var cookieOptions = new CookieOptions
//        {
//            Expires = DateTime.Now.AddHours(1)
//        };


//        Response.Cookies.Append("MonCookie", valeurcookie, cookieOptions);


//        return Content("Cookie ajouté !");
//    }

//    [HttpGet("get")]
//    public IActionResult GetCookie()
//    {

//        var valeurDuCookie = Request.Cookies["MonCookie"];

//        if (valeurDuCookie != null)
//        {
//            return Content($"Valeur du cookie : {valeurDuCookie}");
//        }
//        else
//        {
//            return Content("Cookie non trouvé.");
//        }
//    }

//    [HttpDelete("delete/{nomCookie}")]
//    public IActionResult DeleteCookie(string nomCookie)
//    {
//        var cookie = Request.Cookies[nomCookie];
//        if (cookie == null)
//        {
//            return NotFound("Cookie non trouvé");
//        }

//        Response.Cookies.Delete(nomCookie);
//        return Content("Cookie supprimé avec succès");
//    }
//}


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CookieController : Controller
{
    [HttpGet("set")]
    public IActionResult SetCookie(string valeurcookie)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddSeconds(10),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Append("MonCookie", valeurcookie, cookieOptions);
        return Content("Cookie ajouté !");
    }

    [HttpGet("get")]
    public IActionResult GetCookie()
    {
        var valeurDuCookie = Request.Cookies["MonCookie"];

        if (valeurDuCookie != null)
        {
            return Content($"Valeur du cookie : {valeurDuCookie}");
        }
        else
        {
            return Content("Cookie non trouvé.");
        }
    }

    [HttpDelete("delete/{nomCookie}")]
    public IActionResult DeleteCookie(string nomCookie)
    {
        var cookie = Request.Cookies[nomCookie];
        if (cookie == null)
        {
            return NotFound("Cookie non trouvé");
        }

        Response.Cookies.Delete(nomCookie);
        return Content("Cookie supprimé avec succès");
    }
}
