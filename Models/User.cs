using System.ComponentModel.DataAnnotations;

namespace SurfsupEmil.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public User(string email)
        {
            Email = email;
        }
    }
}
