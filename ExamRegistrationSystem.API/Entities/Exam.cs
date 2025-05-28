namespace ExamRegistrationSystem.Entities;

public class Exam
{
    public int Id { get; set; }
    public string CourseCode { get; set; } = null!;
    public Course Course { get; set; } = null!;

    public int StudentNumber { get; set; }
    public Student Student { get; set; } = null!;

    public DateTime ExamDate { get; set; }
    public int Score { get; set; }
}
