namespace ExamRegistrationSystem.Dto;

public class StudentDto
{
    public int StudentNumber { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Grade { get; set; }
}
