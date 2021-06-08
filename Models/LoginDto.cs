using System.ComponentModel.DataAnnotations;

namespace J6.Models
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
