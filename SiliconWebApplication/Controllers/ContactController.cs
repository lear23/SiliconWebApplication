using Microsoft.AspNetCore.Mvc;

namespace SiliconWebApplication.Controllers;




public class ContactController : Controller
{
    public IActionResult Contact()
    {
        return View();
    }
}
