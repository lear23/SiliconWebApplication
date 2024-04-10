using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconWebApplication.ViewModels.Courses;
using System.Net.Http.Headers;

namespace SiliconWebApplication.Controllers;



public class CoursesController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    //[Authorize]
    //public async Task<IActionResult> Courses()
    //{
    //    var viewModel = new CoursesViewModel();
    //    var response = await _httpClient.GetAsync("https://localhost:7086/api/Course");

    //    if (response.IsSuccessStatusCode)
    //    {
    //        viewModel.CourseModels = await response.Content.ReadAsAsync<IEnumerable<CourseModel>>();
    //    }

    //    return View(viewModel);
    //}




    [Authorize]
    public async Task<IActionResult> Courses()
    {


        var viewModel = new CoursesViewModel();

        var response = await _httpClient.GetAsync("https://localhost:7086/api/Course");

        if (response.IsSuccessStatusCode)
        {
            viewModel.CourseModels = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;
        }

        return View(viewModel);
    }
}







//  var tokenResponse = await _httpClient.SendAsync(new HttpRequestMessage 
//  {
//      RequestUri = new Uri("https://localhost:7086/api/auth"),
//      Method = HttpMethod.Post
//  });
//  if (tokenResponse.IsSuccessStatusCode)
//  {
//      HttpContext.Session.SetString("token", await tokenResponse.Content.ReadAsStringAsync()); 
//  }




//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));