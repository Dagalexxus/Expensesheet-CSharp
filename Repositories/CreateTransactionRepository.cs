
using api.Controllers;
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

        public async Task<Transaction?> DeleteAsync(int id, int userID)
        {
            Transaction? transactionModel = await _context.Transaction.FirstOrDefaultAsync(t => t.Id == id && t.userID == userID);

            if (transactionModel == null){
                return null;
            }

            _context.Transaction.Remove(transactionModel);
            await _context.SaveChangesAsync();

            return transactionModel;
        }

        public async Task<List<Transaction>> GetAllAsync(QueryObject query, int userID)
        {
            IQueryable<Transaction> transactions = _context.Transaction.Where(u => u.userID == userID).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.PaymentProvider)){
                transactions = transactions.Where(t => t.PaymentProvider.Equals(query.PaymentProvider));
            }

            if(!string.IsNullOrWhiteSpace(query.BankType)){
                transactions = transactions.Where(t => t.BankType.Equals(query.BankType));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy)){
                if(query.SortBy.Equals("date", StringComparison.OrdinalIgnoreCase)){
                    transactions = query.IsDescending ? transactions.OrderByDescending(t => t.date) : transactions.OrderBy(t => t.date);
                }
                else if (query.SortBy.Equals("paymentprovider", StringComparison.OrdinalIgnoreCase)){
                    transactions = query.IsDescending ? transactions.OrderByDescending(t => t.PaymentProvider) : transactions.OrderBy(t => t.PaymentProvider);
                }
                else if (query.SortBy.Equals("banktype", StringComparison.OrdinalIgnoreCase)){
                    transactions = query.IsDescending ? transactions.OrderByDescending(t => t.BankType) : transactions.OrderBy(t => t.BankType);
                }
            }

            int skip = (query.PageNumber - 1) * query.PageSize;

            return await transactions.Skip(skip).Take(query.PageSize).ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id, int userID)
        {
            return await _context.Transaction.FirstOrDefaultAsync(t => t.Id == id && t.userID == userID);
        }

        public async Task<Transaction> InsertAsync(Transaction transaction)
        {
            await _context.Transaction.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;        
        }

        public async Task<Transaction?> UpdateAsync(int id, UpdateTransactionRequestDto transactionDto, int userID)
        {
            Transaction? transaction = await _context.Transaction.FirstOrDefaultAsync(t => t.Id == id && t.userID == userID);

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