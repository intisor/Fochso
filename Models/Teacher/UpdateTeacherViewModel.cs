using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Teacher;

public class UpdateTeacherViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Teacher name is required")]
    public string Name { get; set; }
    public string TeachingSubject { get; set; }
}
