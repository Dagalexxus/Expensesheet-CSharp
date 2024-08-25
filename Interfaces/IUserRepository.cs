using api.Models;

namespace api.Interfaces{
    public interface IUserRepository{
        Task<User?> GetByIdAsync(int id);
        Task<User> InsertAsync(Transaction transaction);
    }
}