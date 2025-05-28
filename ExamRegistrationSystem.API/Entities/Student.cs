namespace ExamRegistrationSystem.Entities;

public class Student
{
    public int StudentNumber { get; set; }
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!;
    public int Grade { get; set; } 

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
