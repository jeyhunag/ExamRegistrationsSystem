using ExamRegistrationSystem.Dto;
using ExamRegistrationSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExamRegistrationSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IExamService _service;

    public ExamsController(IExamService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(new ResponseDto { Result = result });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(new ResponseDto { Result = result });
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> Create([FromBody] ExamDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(new ResponseDto { Result = result, Message = "İmtahan uğurla yaradıldı." });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto>> Update(int id, [FromBody] ExamDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return Ok(new ResponseDto { Result = result, Message = "İmtahan uğurla yeniləndi." });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto>> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new ResponseDto { Message = "İmtahan uğurla silindi." });
    }
}
