using AutoMapper;
using ExamRegistrationSystem.Data;
using ExamRegistrationSystem.Dto;
using ExamRegistrationSystem.Entities;
using ExamRegistrationSystem.Exceptions;
using ExamRegistrationSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ExamRegistrationSystem.Services;

public class ExamService : IExamService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ExamService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ExamDto>> GetAllAsync()
    {
        var exams = await _context.Exams.ToListAsync();
        return _mapper.Map<List<ExamDto>>(exams);
    }

    public async Task<ExamDto> GetByIdAsync(int id)
    {
        var exam = await _context.Exams.FindAsync(id);
        if (exam == null)
            throw new CustomException("İmtahan tapılmadı.");

        return _mapper.Map<ExamDto>(exam);
    }

    public async Task<ExamDto> CreateAsync(ExamDto dto)
    {
        var course = await _context.Courses.FindAsync(dto.CourseCode);
        var student = await _context.Students.FindAsync(dto.StudentNumber);
        if (course == null || student == null)
            throw new CustomException("Dərs və ya şagird mövcud deyil.");

        var exam = _mapper.Map<Exam>(dto);
        _context.Exams.Add(exam);
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<ExamDto> UpdateAsync(int id, ExamDto dto)
    {
        var exam = await _context.Exams.FindAsync(id);
        if (exam == null)
            throw new CustomException("İmtahan tapılmadı.");

        var course = await _context.Courses.FindAsync(dto.CourseCode);
        var student = await _context.Students.FindAsync(dto.StudentNumber);
        if (course == null || student == null)
            throw new CustomException("Dərs və ya şagird mövcud deyil.");

        exam.CourseCode = dto.CourseCode;
        exam.StudentNumber = dto.StudentNumber;
        exam.ExamDate = dto.ExamDate;
        exam.Score = dto.Score;

        _context.Entry(exam).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return _mapper.Map<ExamDto>(exam);
    }


    public async Task DeleteAsync(int id)
    {
        var exam = await _context.Exams.FindAsync(id);
        if (exam == null)
            throw new CustomException("İmtahan tapılmadı.");

        _context.Exams.Remove(exam);
        await _context.SaveChangesAsync();
    }
}
