using System.ComponentModel.DataAnnotations;
using api.DataAnnotations;

namespace api.Dtos.Transaction{
    public class UpdateTransactionRequestDto{
        [Required]
        public int userID {get; set;}

        [Required]
        [PermittedValues(["Amex","Mastercard", "Visa"])]
        public string PaymentProvider {get; set;} = string.Empty;

        [Required]
        [MaxLength(60)]
        public string BankType {get; set;} = string.Empty;

        [Required]
        [MaxLength(60)]
        public string Category {get; set;} = string.Empty;

        [Required]
        [Range(0.01, 1000000.00)]
        public decimal Amount {get; set;}
        
        [MaxLength(280, ErrorMessage = "Comment can not be longer than 280 characters")]
        public string Comment {get; set;} = string.Empty;
    }
}