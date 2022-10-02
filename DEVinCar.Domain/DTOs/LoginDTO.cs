using System.ComponentModel.DataAnnotations;
using DEVinCar.Domain.Annotations;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.DTOs{
    public class LoginDTO {

        [Required(ErrorMessage = "The email is required")]
        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [MaxLength(50)]
        [MinLength(4, ErrorMessage = "The password must contain at least 4 digits")]
        [DistinctCharactersAttribute]
        public string Password { get; set; }

        public LoginDTO()
        {
            
        }
    }
}