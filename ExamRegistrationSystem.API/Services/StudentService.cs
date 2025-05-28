using AutoMapper;
using ExamRegistrationSystem.Data;
using ExamRegistrationSystem.Dto;
using ExamRegistrationSystem.Entities;
using ExamRegistrationSystem.Exceptions;
using ExamRegistrationSystem.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ExamRegistrationSystem.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public StudentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<StudentDto>> GetAllAsync()
    {
        var students = await _context.Students.ToListAsync();
        return _mapper.Map<List<StudentDto>>(students);
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            throw new CustomException("Şagird tapılmadı.");

        return _mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto> CreateAsync(StudentDto dto)
    {
        var student = _mapper.Map<Student>(dto);
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<StudentDto> UpdateAsync(int id, StudentDto dto)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            throw new CustomException("Şagird tapılmadı.");

        dto.StudentNumber = id;

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.Grade = dto.Grade;

        _context.Entry(student).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return _mapper.Map<StudentDto>(student);
    }



    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            throw new CustomException("Şagird tapılmadı.");

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}
