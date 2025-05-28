namespace ExamRegistrationSystem.Dto;

public class CourseDto
{
    public string CourseCode { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public int Grade { get; set; }
    public string TeacherFirstName { get; set; } = null!;
    public string TeacherLastName { get; set; } = null!;
}
