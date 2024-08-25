using api.Models;

namespace api.Interfaces{
    public interface IUserRepository{
        Task<User?> GetByUserIdAsync(int id);
        Task<User> RegisterAsync(Transaction transaction);
    }
}