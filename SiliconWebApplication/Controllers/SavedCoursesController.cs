using SiliconWebApplication.ViewModels.Account;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconWebApplication.ViewModels.Courses;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace SiliconWebApp.Controllers
{
    public class SavedCoursesController(UserManager<UserEntity> userManager, HttpClient httpClient, AppDbContext dbContext) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly HttpClient _httpClient = httpClient;
        private readonly AppDbContext _dbContext = dbContext;


        public async Task<IActionResult> SavedCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new AccountDetailsViewModel
            {
                ProfileInfo = new ProfileInfoViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!
                }
            };

            // Obtener los cursos guardados del usuario y mapearlos a SavedCourseViewModel
            var savedCourses = await _dbContext.Courses.ToListAsync();
            viewModel.SavedCourses = savedCourses.Select(course => new SavedCourseViewModel
            {
                Title = course.Title,
                Author = course.Author,
                ImageName = course.ImageName,
                Price = course.Price,
                Discount = course.Discount,
                Hours = course.Hours,
                IsBestSeller = course.IsBestSeller,
                LikesNumbers = course.LikesNumbers,
                LikesProcent = course.LikesProcent
            }).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCourse(string courseId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7086/api/course/{courseId}");

            if (response.IsSuccessStatusCode)
            {
                var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());

                // Guardar el curso en la base de datos
                var courseEntity = new CourseEntity
                {
                    // Mapea los valores del curso recibido desde la API al modelo de entidad
                    Title = course.Title,
                    Author = course.Author,
                    ImageName = course.ImageName,
                    Price = course.Price,
                    Discount = course.Discount,
                    Hours = course.Hours,
                    IsBestSeller = course.IsBestSeller,
                    LikesNumbers = course.LikesNumbers.ToString(),
                    LikesProcent = course.LikesProcent.ToString(),
                };

                _dbContext.Courses.Add(courseEntity);
                await _dbContext.SaveChangesAsync();

                // Redireccionar a la acción SavedCourses en el controlador SavedCoursesController
                return RedirectToAction("SavedCourses", "SavedCourses");
            }
            else
            {
                return View("Error");
            }
        }



      

    }
}





//public async Task<IActionResult> SavedCourses()
//{
//    var user = await _userManager.GetUserAsync(User);
//    if (user == null)
//    {
//        return RedirectToAction("Login", "Account");
//    }

//    var viewModel = new AccountDetailsViewModel
//    {
//        ProfileInfo = new ProfileInfoViewModel
//        {
//            FirstName = user.FirstName,
//            LastName = user.LastName,
//            Email = user.Email!
//        }
//    };    // Obtener los cursos guardados del usuario y mapearlos a SavedCourseViewModel
//    var savedCourses = await _dbContext.Courses.ToListAsync();
//    viewModel.SavedCourses = savedCourses.Select(course => new SavedCourseViewModel
//    {
//        Title = course.Title,
//        Author = course.Author,
//        ImageName = course.ImageName,
//        Price = course.Price,
//        Discount = course.Discount,
//        Hours = course.Hours,
//        IsBestSeller = course.IsBestSeller,
//        LikesNumbers = course.LikesNumbers,
//        LikesProcent = course.LikesProcent
//    }).ToList();

//    return View(viewModel);


//}


//[HttpPost]
//public async Task<IActionResult> SaveCourse(string courseId)
//{
//    var response = await _httpClient.GetAsync($"https://localhost:7086/api/course/{courseId}");

//    if (response.IsSuccessStatusCode)
//    {
//        var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());

//        // Guardar el curso en la base de datos
//        var courseEntity = new CourseEntity
//        {
//            // Mapea los valores del curso recibido desde la API al modelo de entidad
//            Title = course.Title,
//            Author = course.Author,
//            ImageName = course.ImageName,
//            Price = course.Price,
//            Discount = course.Discount,
//            Hours = course.Hours,
//            IsBestSeller = course.IsBestSeller,
//            LikesNumbers = course.LikesNumbers,
//            LikesProcent = course.LikesProcent,

//        };

//        _dbContext.Courses.Add(courseEntity);
//        await _dbContext.SaveChangesAsync();

//        return RedirectToAction("SavedCourses");
//    }
//    else
//    {
//        return View("Error");
//    }
//}