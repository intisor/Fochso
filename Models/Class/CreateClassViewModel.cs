using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Class;

public class CreateClassViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Class name is required")]
    public string Name { get; set; }
    public string Description { get; set; }

}
