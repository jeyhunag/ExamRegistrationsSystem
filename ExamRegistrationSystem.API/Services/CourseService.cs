using AutoMapper;
using ExamRegistrationSystem.Data;
using ExamRegistrationSystem.Dto;
using ExamRegistrationSystem.Entities;
using ExamRegistrationSystem.Exceptions;
using ExamRegistrationSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CourseService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CourseDto>> GetAllAsync()
    {
        var courses = await _context.Courses.ToListAsync();
        return _mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<CourseDto> GetByCodeAsync(string code)
    {
        var course = await _context.Courses.FindAsync(code);
        if (course == null)
            throw new CustomException("Dərs tapılmadı.");

        return _mapper.Map<CourseDto>(course);
    }

    public async Task<CourseDto> CreateAsync(CourseDto dto)
    {
        var course = _mapper.Map<Course>(dto);
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<CourseDto> UpdateAsync(string code, CourseDto dto)
    {
        var course = await _context.Courses.FindAsync(code);
        if (course == null)
            throw new CustomException("Dərs tapılmadı.");

        dto.CourseCode = code;

        course.CourseName = dto.CourseName;
        course.Grade = dto.Grade;
        course.TeacherFirstName = dto.TeacherFirstName;
        course.TeacherLastName = dto.TeacherLastName;

        _context.Entry(course).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return _mapper.Map<CourseDto>(course);
    }


    public async Task DeleteAsync(string code)
    {
        var course = await _context.Courses.FindAsync(code);
        if (course == null)
            throw new CustomException("Dərs tapılmadı.");

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }
}
