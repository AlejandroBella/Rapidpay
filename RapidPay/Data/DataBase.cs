using Microsoft.EntityFrameworkCore;
using RapidPay.Data.Model;

namespace RapidPay.Data
{
    public class Database : DbContext
    {
        protected readonly IConfiguration Configuration;

        public Database(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("database"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CardModel>()
                .HasKey(x => x.Number);

            modelBuilder.Entity<BalanceModel>()
                .HasKey(x => x.BalanceId);

            modelBuilder.Entity<BalanceDetailModel>()
                .HasKey(x => x.DetailId);

            //modelBuilder.Entity<BalanceModel>()
            //    .HasMany(x => x.Detail)
            //    .WithOne(m => m.Balance)
            //    .HasForeignKey(m => m.BalanceId);

        }
        public DbSet<CardModel> CreditCard => Set<CardModel>();
        public DbSet<BalanceModel> Balance => Set<BalanceModel>();
        public DbSet<BalanceDetailModel> BalanceDetail => Set<BalanceDetailModel>();


    }
}
