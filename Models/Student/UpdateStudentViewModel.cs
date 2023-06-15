using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Student;

public class UpdateStudentViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Student's Id name is required")]
    public string Name { get; set; }
    public string Class { get; set; }
}
