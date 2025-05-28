namespace ExamRegistrationSystem.Dto;

public class ExamDto
{
    public int ExamId { get; set; }
    public string CourseCode { get; set; } = null!;
    public int StudentNumber { get; set; }
    public DateTime ExamDate { get; set; }
    public int Score { get; set; }
}
