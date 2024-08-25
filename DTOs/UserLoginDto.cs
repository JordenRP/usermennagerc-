using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
