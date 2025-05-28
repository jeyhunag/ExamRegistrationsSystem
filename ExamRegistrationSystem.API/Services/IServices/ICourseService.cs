using ExamRegistrationSystem.Dto;

namespace ExamRegistrationSystem.Services.IServices;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync();
    Task<CourseDto> GetByCodeAsync(string code);
    Task<CourseDto> CreateAsync(CourseDto dto);
    Task<CourseDto> UpdateAsync(string code, CourseDto dto);
    Task DeleteAsync(string code);
}
