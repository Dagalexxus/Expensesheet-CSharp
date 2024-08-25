using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models{
    public class User{
        [Key]
        public int id {get; set;}

        [Required]
        public DateTime signUp {get; set;} = DateTime.Now;

        [Required]
        [MinLength(length:6, ErrorMessage = "Minimum username length is 6 characters")]
        [MaxLength(length:25, ErrorMessage = "Username cannot be longer than 25 characters")]
        public string username {get; set;} = string.Empty;

        [Required]
        public string passwordHash{get; set;} = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "This has to be a valid email address")]
        public string userEmail{get; set;} = string.Empty;

    }
}