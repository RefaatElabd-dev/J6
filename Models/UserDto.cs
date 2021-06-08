using System.ComponentModel.DataAnnotations;

namespace J6.Models
{
    public class UserDto
    {
        
        public string UserName { get; set; }
        
        public string Token { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
