using ExamRegistrationSystem.Dto;

namespace ExamRegistrationSystem.Services.IServices;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllAsync();
    Task<StudentDto> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(StudentDto dto);
    Task<StudentDto> UpdateAsync(int id, StudentDto dto);
    Task DeleteAsync(int id);
}
