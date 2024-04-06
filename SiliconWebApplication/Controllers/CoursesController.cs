using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconWebApplication.ViewModels.Courses;

namespace SiliconWebApplication.Controllers;



public class CoursesController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;


    public async Task<IActionResult> Courses()
    {
        var viewModel = new CoursesViewModel();


        var response = await _httpClient.GetAsync("https://localhost:7086/api/Course");
   


        viewModel.CourseModels = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;

        return View(viewModel);
    }
}



//var response = await _httpClient.GetAsync("https://localhost:7071/api/CourseApi");

