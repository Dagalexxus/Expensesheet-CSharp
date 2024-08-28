using api.Dtos.Transaction;
using api.Models;

namespace api.Mappers{
    public static class TransactionMapper{
        public static TransactionDTO toTransactionDTO(this Transaction transactionModel){
            return new TransactionDTO{
                date = transactionModel.date,
                PaymentProvider = transactionModel.PaymentProvider,
                BankType = transactionModel.BankType,
                Category = transactionModel.Category,
                Amount = transactionModel.Amount,
                Comment = transactionModel.Comment
            };
        }

        public static Transaction ToTransactionFromInsertDto(this InsertTransactionRequestDto insertDTO){
            return new Transaction{
                date = insertDTO.date,
                PaymentProvider = insertDTO.PaymentProvider,
                BankType = insertDTO.BankType,
                Category = insertDTO.Category,
                Amount = insertDTO.Amount,
                Comment = insertDTO.Comment
            };
        }
    }
}