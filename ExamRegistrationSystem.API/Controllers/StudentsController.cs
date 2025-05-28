using ExamRegistrationSystem.Dto;
using ExamRegistrationSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExamRegistrationSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(new ResponseDto { Result = result });
    }

    [HttpGet("{studentNumber}")]
    public async Task<ActionResult<ResponseDto>> GetByNumber(int studentNumber)
    {
        var result = await _service.GetByIdAsync(studentNumber);
        return Ok(new ResponseDto { Result = result });
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> Create([FromBody] StudentDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(new ResponseDto { Result = result, Message = "Şagird uğurla yaradıldı." });
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto>> Update(int id, [FromBody] StudentDto dto)
    {
        dto.StudentNumber = id;
        var result = await _service.UpdateAsync(id, dto);
        return Ok(new ResponseDto { Result = result, Message = "Şagird uğurla yeniləndi." });
    }


    [HttpDelete("{studentNumber}")]
    public async Task<ActionResult<ResponseDto>> Delete(int studentNumber)
    {
        await _service.DeleteAsync(studentNumber);
        return Ok(new ResponseDto { Message = "Şagird uğurla silindi." });
    }
}
