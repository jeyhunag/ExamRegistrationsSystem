using ExamRegistrationSystem.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        _baseUrl = configuration["ApiBaseUrl"];
    }

    public async Task<IActionResult> Index()
    {
        var students = await GetCountAsync("students");
        var courses = await GetCountAsync("courses");
        var exams = await GetCountAsync("exams");

        ViewBag.StudentCount = students;
        ViewBag.CourseCount = courses;
        ViewBag.ExamCount = exams;

        return View();
    }

    private async Task<int> GetCountAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/{endpoint}");
        if (!response.IsSuccessStatusCode) return 0;

        var json = await response.Content.ReadAsStringAsync();
        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseDto>(json);
        var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(data?.Result?.ToString() ?? "[]");

        return list.Count;
    }
}
