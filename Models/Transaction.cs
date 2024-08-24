using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models{

    public class Transaction{
        [Key]
        public int Id {get; set;}
        public DateOnly date {get; set;}
        public int userID {get; set;}
        public string PaymentProvider {get; set;} = string.Empty;
        public string BankType{get; set;} = string.Empty; //which bank?
        public string Category {get; set;} = string.Empty;

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Amount {get; set;}

        public string Comment {get; set;} = string.Empty;
    }
};