using api.Dtos.Transaction;
using api.Models;

namespace api.Mappers{
    public static class TransactionMapper{
        public static TransactionDTO toTransactionDTO(this Transaction transactionModel){
            return new TransactionDTO{
                Id = transactionModel.Id,
                date = transactionModel.date,
                PaymentProvider = transactionModel.PaymentProvider,
                Category = transactionModel.Category,
                Amount = transactionModel.Amount,
                Comment = transactionModel.Comment
            };
        }

        public static Transaction ToTransactionFromInsertDto(this InsertTransactionRequestDto insertDTO){
            return new Transaction{
                userID = insertDTO.userID,
                PaymentProvider = insertDTO.PaymentProvider,
                Category = insertDTO.Category,
                Amount = insertDTO.Amount,
                Comment = insertDTO.Comment
            };
        }
    }
}