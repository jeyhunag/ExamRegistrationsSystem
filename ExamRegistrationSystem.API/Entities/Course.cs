namespace ExamRegistrationSystem.Entities;

public class Course
{
    public string CourseCode { get; set; } = null!; 
    public string CourseName { get; set; } = null!;
    public int Grade { get; set; } 
    public string TeacherFirstName { get; set; } = null!;
    public string TeacherLastName { get; set; } = null!; 

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
