using ExamRegistrationSystem.Dto;
using ExamRegistrationSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExamRegistrationSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;

    public CoursesController(ICourseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(new ResponseDto { Result = result });
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<ResponseDto>> GetByCode(string code)
    {
        var result = await _service.GetByCodeAsync(code);
        return Ok(new ResponseDto { Result = result });
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> Create([FromBody] CourseDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(new ResponseDto { Result = result, Message = "Dərs uğurla yaradıldı." });
    }

    [HttpPut("{code}")]
    public async Task<ActionResult<ResponseDto>> Update(string code, [FromBody] CourseDto dto)
    {
        var result = await _service.UpdateAsync(code, dto);
        return Ok(new ResponseDto { Result = result, Message = "Dərs uğurla yeniləndi." });
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult<ResponseDto>> Delete(string code)
    {
        await _service.DeleteAsync(code);
        return Ok(new ResponseDto { Message = "Dərs uğurla silindi." });
    }
}
