using System.ComponentModel.DataAnnotations;

namespace api.Dtos{
    public class RegisterUserRequestDto{
        [Required]
        [MinLength(length:6, ErrorMessage = "Minimum username length is 6 characters")]
        [MaxLength(length:25, ErrorMessage = "Username cannot be longer than 25 characters")]
        public string username {get; set;} = string.Empty;

        // need to define password requirements better
        [Required]
        public string passwordHash{get; set;} = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "This has to be a valid email address")]
        public string userEmail{get; set;} = string.Empty;
    }
}