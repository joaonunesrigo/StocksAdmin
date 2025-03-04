using Microsoft.EntityFrameworkCore;
using StocksAdmin.Api.Entities;

namespace StocksAdmin.Api.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<User> Users { get; set; }
    }
}

