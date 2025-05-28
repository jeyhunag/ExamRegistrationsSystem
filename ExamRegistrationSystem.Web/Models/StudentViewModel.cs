using System.ComponentModel.DataAnnotations;

namespace ExamRegistrationSystem.Web.Models;

public class StudentViewModel
{
    public int StudentNumber { get; set; }
    [Required(ErrorMessage = "Ad boş ola bilməz.")]
    [MaxLength(20, ErrorMessage = "Ad maksimum 20 simvol ola bilər.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Soyad boş ola bilməz.")]
    [MaxLength(20, ErrorMessage = "Soyad maksimum 20 simvol ola bilər.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Sinif boş ola bilməz.")]
    [Range(0, 99, ErrorMessage = "Sinif maksimum 2 rəqəmli olmalıdır.")]
    public int Grade { get; set; }
}

