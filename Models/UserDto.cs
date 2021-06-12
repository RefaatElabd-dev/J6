using System.ComponentModel.DataAnnotations;

namespace J6.Models
{
    public class UserDto
    {
        
        public string UserName { get; set; }
<<<<<<< HEAD
        
=======
        public int Id { get; set; }
>>>>>>> cda8e4c6c7f9f41f927f342ee2d1a7c051d7ae4b
        public string Token { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
