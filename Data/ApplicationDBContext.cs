using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data{
    public class ApplicationDBContext : DbContext{
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Transaction> Transaction {get; set;}
        public DbSet<User> Users {get; set;}

    }
}