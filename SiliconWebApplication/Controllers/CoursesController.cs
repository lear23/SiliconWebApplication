using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiliconWebApplication.ViewModels.Courses;


namespace SiliconWebApplication.Controllers;


public class CoursesController(CategoryService service, CourseService courseService) : Controller
{
    private readonly CategoryService _service = service;
    private readonly CourseService _courseService = courseService;


    //[Authorize]
    public async Task<IActionResult> Courses(string category ="", string searchQuery = "")
    {


        var viewModel = new CoursesViewModel
        {
            Category = await _service.GetCategoriesAsync(),
            CourseModels = await _courseService.GetCourseAsync(category, searchQuery),
        };


        return View(viewModel);
    }


}





//-------------DEN HÄR FUNKAR OCKSÅ ---------

//    [Authorize]
//    public async Task<IActionResult> Courses()
//    {


//        var viewModel = new CoursesViewModel();

//        var response = await _httpClient.GetAsync("https://localhost:7086/api/Course");

//        if (response.IsSuccessStatusCode)
//        {
//            viewModel.CourseModels = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;
//        }

//        return View(viewModel);
//    }







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