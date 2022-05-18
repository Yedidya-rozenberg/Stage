using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; }

       [Required]  public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
        [Required] public string Email { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 chars")]
        public string Password { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;

        
    }
}