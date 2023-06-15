using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Teacher;

public class CreateTeacherViewModel
{
    [Required(ErrorMessage = "Teacher name is required")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string TeachingSubject { get; set; }
}
