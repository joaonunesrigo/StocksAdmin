using Microsoft.EntityFrameworkCore;
using ProductClientHub.Api.Entities;
using ProjectClientHub.Entities;

namespace ProductClientHub.Api.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<User> Users { get; set; }
    }
}

