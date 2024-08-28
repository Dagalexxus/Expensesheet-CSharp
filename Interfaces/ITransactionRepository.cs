
using api.Controllers;
using api.Dtos.Transaction;
using api.Models;

namespace api.Interfaces{
    public interface ITransactionRepository{
        Task<List<Transaction>> GetAllAsync(QueryObject query, int userID);
        Task<Transaction?> GetByIdAsync(int id, int userID);
        Task<Transaction> InsertAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync(int id, UpdateTransactionRequestDto transactionDto, int userID);
        Task<Transaction?> DeleteAsync(int id, int userID);
    }
}