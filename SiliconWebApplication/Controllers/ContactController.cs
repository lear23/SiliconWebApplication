using Microsoft.AspNetCore.Mvc;

namespace SiliconWebApplication.Controllers;



[Route("/contact")]
public class ContactController : Controller
{
    public IActionResult Contact()
    {
        return View();
    }
}
