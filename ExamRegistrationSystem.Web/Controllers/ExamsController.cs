using ExamRegistrationSystem.Web.Models;
using ExamRegistrationSystem.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ExamRegistrationSystem.Web.Controllers
{
    public class ExamsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExamsController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/exams");
            if (!response.IsSuccessStatusCode)
                return View(new List<ExamViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            var exams = JsonConvert.DeserializeObject<List<ExamViewModel>>(data?.Result?.ToString() ?? "[]");

            return View(exams);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Students = await GetStudentsAsync();
            ViewBag.Courses = await GetCoursesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/exams", content);

            if (!response.IsSuccessStatusCode)
                return View(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/exams/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            var exam = JsonConvert.DeserializeObject<ExamViewModel>(data?.Result?.ToString() ?? "");

            ViewBag.Students = await GetStudentsAsync();
            ViewBag.Courses = await GetCoursesAsync();
            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExamViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/api/exams/{model.ExamId}", content);

            if (!response.IsSuccessStatusCode)
                return View(model);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/exams/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            var exam = JsonConvert.DeserializeObject<ExamViewModel>(data?.Result?.ToString() ?? "");

            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/api/exams/{id}");
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<StudentViewModel>> GetStudentsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/students");
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            return JsonConvert.DeserializeObject<List<StudentViewModel>>(data?.Result?.ToString() ?? "[]");
        }

        private async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/courses");
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            return JsonConvert.DeserializeObject<List<CourseViewModel>>(data?.Result?.ToString() ?? "[]");
        }
    }
}
