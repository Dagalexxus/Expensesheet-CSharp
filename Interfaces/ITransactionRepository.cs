
using api.Dtos.Transaction;
using api.Models;

namespace api.Interfaces{
    public interface ITransactionRepository{
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(int id);
        Task<Transaction> InsertAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync(int id, UpdateTransactionRequestDto transactionDto);
        Task<Transaction?> DeleteAsync(int id);
    }
}