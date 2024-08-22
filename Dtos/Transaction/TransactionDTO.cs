
namespace api.Dtos.Transaction{
    public class TransactionDTO{
        public int Id {get; set;}
        public DateTime date {get; set;} = DateTime.Now;

        //remove user id for testing purposes
        public string PaymentProvider {get; set;} = string.Empty;
        public string Category {get; set;} = string.Empty;
        public decimal Amount {get; set;}

        public string Comment {get; set;} = string.Empty;
    
    }
}