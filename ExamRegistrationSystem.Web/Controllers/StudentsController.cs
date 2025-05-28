using ExamRegistrationSystem.Web.Models;
using ExamRegistrationSystem.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ExamRegistrationSystem.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public StudentsController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/students");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Şagirdləri gətirə bilmədik.";
                return View(new List<StudentViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);

            if (data?.Result == null)
                return View(new List<StudentViewModel>());

            var students = JsonConvert.DeserializeObject<List<StudentViewModel>>(data.Result.ToString());
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/students", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Yaratma zamanı xəta baş verdi.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/students/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            var student = JsonConvert.DeserializeObject<StudentViewModel>(data.Result.ToString());

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/api/students/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Yeniləmə zamanı xəta baş verdi.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/students/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(json);
            var student = JsonConvert.DeserializeObject<StudentViewModel>(data.Result.ToString());

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/students/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
