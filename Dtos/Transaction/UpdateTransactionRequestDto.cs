namespace api.Dtos.Transaction{
    public class UpdateTransactionRequestDto{
        public int userID {get; set;}
        public string PaymentProvider {get; set;} = string.Empty;
        public string Category {get; set;} = string.Empty;
        public decimal Amount {get; set;}
        public string Comment {get; set;} = string.Empty;
    }
}