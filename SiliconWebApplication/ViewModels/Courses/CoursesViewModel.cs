using System.ComponentModel.DataAnnotations.Schema;

namespace SiliconWebApplication.ViewModels.Courses;

public class CoursesViewModel
{
    public IEnumerable<CourseModel> CourseModels { get; set; } = []; 
}
