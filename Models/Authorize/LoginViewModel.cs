using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Authorize
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
