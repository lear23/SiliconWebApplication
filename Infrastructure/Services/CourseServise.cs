
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class CourseService(HttpClient http, IConfiguration config)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _config = config;


    public async Task<IEnumerable<CourseModel>> GetCourseAsync(string category = "", string searchQuery = "")
    {
        var response = await _http.GetAsync($"{_config["ApiUris:Courses"]}?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}");
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());
            if (result != null && result.Succeeded)
            {
                return result.Courses ??= null!;
            }


            return null!;
        }
        return null!;
    }



}


//using Infrastructure.Models;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using System.Net.Http;

//namespace Infrastructure.Services
//{
//    public class CourseService
//    {
//        private readonly HttpClient _http;
//        private readonly IConfiguration _config;

//        public CourseService(HttpClient http, IConfiguration config)
//        {
//            _http = http;
//            _config = config;

//            // Configurar el BaseAddress del HttpClient
//            _http.BaseAddress = new Uri(_config["ApiUris:Courses"]!);
//        }

//        public async Task<IEnumerable<CourseModel>> GetCourseAsync()
//        {
//            var response = await _http.GetAsync("");
//            if (response.IsSuccessStatusCode)
//            {
//                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync());
//                return courses!;
//            }
//            return null!; // Aquí simplemente usamos "return null" ya que no es necesario utilizar null!
//        }
//    }
//}
