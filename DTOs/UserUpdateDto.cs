using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.DTOs
{
    public class UserUpdateDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
