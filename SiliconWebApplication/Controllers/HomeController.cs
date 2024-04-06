using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Threading.Tasks;

namespace SiliconWebApplication.Controllers;

public class HomeController : Controller
{
    public IActionResult Home()
    {

        return View();
    }

    public IActionResult Error404(int statusCode)
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Home(SubscriberEntity model) 
    {
        if (ModelState.IsValid)
        {
            try
            {
                using var http = new HttpClient();
                var json = JsonConvert.SerializeObject(model);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.PostAsync($"https://localhost:7071/api/Subscriber?Key=ZTQzNWY2NTYtMjMxNi00NDRlLWFjMmEtN2I0NzU5YWNmZWM5&email={model.Email}", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                    ModelState.Clear();

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "AlreadyExists";
                } 
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewData["Status"] = "Unauthorized";
                }
            }
            catch
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }
        else
        {
            ViewData["status"] = "Invalid";
        }

        return View(model);
    }
}
