using ExamRegistrationSystem.Dto;

namespace ExamRegistrationSystem.Services.IServices;

public interface IExamService
{
    Task<List<ExamDto>> GetAllAsync();
    Task<ExamDto> GetByIdAsync(int id);
    Task<ExamDto> CreateAsync(ExamDto dto);
    Task<ExamDto> UpdateAsync(int id, ExamDto dto);
    Task DeleteAsync(int id);
}
