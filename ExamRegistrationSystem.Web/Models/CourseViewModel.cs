using System.ComponentModel.DataAnnotations;

namespace ExamRegistrationSystem.Web.Models;

public class CourseViewModel
{
    [Required(ErrorMessage = "Dərsin kodu boş ola bilməz.")]
    [MaxLength(5, ErrorMessage = "Dərsin kodu maksimum 5 simvol ola bilər.")]
    public string CourseCode { get; set; } = null!;

    [Required(ErrorMessage = "Dərsin adı boş ola bilməz.")]
    [MaxLength(25, ErrorMessage = "Dərsin adı maksimum 25 simvol ola bilər.")]
    public string CourseName { get; set; } = null!;

    [Required(ErrorMessage = "Sinif seçilməlidir.")]
    [Range(0, 99, ErrorMessage = "Sinif maksimum 2 rəqəmli olmalıdır.")]
    public int Grade { get; set; }

    [Required(ErrorMessage = "Müəllimin adı boş ola bilməz.")]
    [MaxLength(20, ErrorMessage = "Müəllimin adı maksimum 20 simvol ola bilər.")]
    public string TeacherFirstName { get; set; } = null!;

    [Required(ErrorMessage = "Müəllimin soyadı boş ola bilməz.")]
    [MaxLength(20, ErrorMessage = "Müəllimin soyadı maksimum 20 simvol ola bilər.")]
    public string TeacherLastName { get; set; } = null!;
}
