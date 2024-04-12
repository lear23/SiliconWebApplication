using Infrastructure.Models;


namespace SiliconWebApplication.ViewModels.Courses;

public class CoursesViewModel
{
    //public IEnumerable<CourseModel> CourseModels { get; set; } = []; 
    public IEnumerable<CourseModel>? CourseModels { get; set; } 
    public IEnumerable<CategoryModel>? Category { get; set; } 
}
