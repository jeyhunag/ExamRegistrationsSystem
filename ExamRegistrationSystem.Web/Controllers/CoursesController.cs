using ExamRegistrationSystem.Web.Models;
using ExamRegistrationSystem.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ExamRegistrationSystem.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CoursesController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/courses");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Dərsləri gətirə bilmədik.";
                return View(new List<CourseViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            if (data?.Result == null)
                return View(new List<CourseViewModel>());

            var courses = JsonConvert.DeserializeObject<List<CourseViewModel>>(data.Result.ToString()!);
            return View(courses);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/courses", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Dərs əlavə olunmadı.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/courses/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            var course = JsonConvert.DeserializeObject<CourseViewModel>(data!.Result!.ToString()!);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CourseViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/api/courses/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Dəyişiklik edilmədi.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/courses/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            var course = JsonConvert.DeserializeObject<CourseViewModel>(data!.Result!.ToString()!);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/api/courses/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
