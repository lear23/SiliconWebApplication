using SiliconWebApplication.ViewModels.Account;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconWebApplication.ViewModels.Courses;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;
using SiliconWebApplication.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace SiliconWebApp.Controllers
{

    [Authorize]
    public class SavedCoursesController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _dbContext;

        public SavedCoursesController(UserManager<UserEntity> userManager, HttpClient httpClient, AppDbContext dbContext)
        {
            _userManager = userManager;
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> SavedCourses()
        {
            try
            {
                if (ModelState.IsValid)
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

                    var savedCourses = await _dbContext.Courses.ToListAsync();
                    viewModel.SavedCourses = savedCourses.Select(course => new SavedCourseViewModel
                    {
                        Title = course.Title,
                        Author = course.Author!,
                        ImageName = course.ImageName!,
                        Price = course.Price,
                        Discount = course.Discount,
                        Hours = course.Hours,
                        IsBestSeller = course.IsBestSeller,
                        LikesNumbers = course.LikesNumbers,
                        LikesProcent = course.LikesProcent
                    }).ToList();

                    return View(viewModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
               
                return View("Error404");
            }
        }

        [HttpPost]
        [Route("/SavedCourses")]
        public async Task<IActionResult> SaveCourse(string courseId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    var response = await _httpClient.GetAsync($"https://localhost:7086/api/course/{courseId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());


                        var courseEntity = new CourseEntity
                        {
                            Title = course!.Title,
                            Author = course.Author,
                            ImageName = course.ImageName,
                            Price = course.Price,
                            Discount = course.Discount,
                            Hours = course.Hours,
                            IsBestSeller = course.IsBestSeller,
                            LikesNumbers = course.LikesNumbers!.ToString(),
                            LikesProcent = course.LikesProcent!.ToString(),
                            UserId = user.Id
                        };

                        _dbContext.Courses.Add(courseEntity);
                        await _dbContext.SaveChangesAsync();

                        return RedirectToAction("SavedCourses", "SavedCourses");
                    }
                    else
                    {
                        return View("Error404");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
               
                return View("Error");
            }
        }


        #region Delete
        [HttpPost]
        [Route("/SavedCourses/DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var savedCourses = await _dbContext.Courses.Where(course => course.UserId == user.Id).ToListAsync();
                _dbContext.Courses.RemoveRange(savedCourses);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("SavedCourses", "SavedCourses");
            }
            catch (Exception ex)
            {
                return View("Error404");
            }
        }
        #endregion



    }
}

