
using api.Data;
using api.Dtos.Transaction;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDBContext _context;
        public TransactionRepository(ApplicationDBContext dBContext){
            _context = dBContext;
        }

        public async Task<Transaction?> DeleteAsync(int id)
        {
            Transaction? transactionModel = await _context.Transaction.FindAsync(id);

            if (transactionModel == null){
                return null;
            }

            _context.Transaction.Remove(transactionModel);
            await _context.SaveChangesAsync();

            return transactionModel;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transaction.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transaction.FindAsync(id);
        }

        public async Task<Transaction> InsertAsync(Transaction transaction)
        {
            await _context.Transaction.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;        
        }

        public async Task<Transaction?> UpdateAsync(int id, UpdateTransactionRequestDto transactionDto)
        {
            Transaction? transaction = await _context.Transaction.FindAsync(id);

            if (transaction == null){
                return null;
            }

            transaction.Amount = transactionDto.Amount;
            transaction.Category = transactionDto.Category;
            transaction.Comment = transactionDto.Comment;
            transaction.PaymentProvider = transactionDto.PaymentProvider;
            transaction.userID = transactionDto.userID;

            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}