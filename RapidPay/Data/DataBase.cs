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

            modelBuilder.Entity<CardModel>()
                .HasOne(x=>x.Balance)
                .WithOne(m=>m.Card)
                .HasForeignKey(typeof(CardModel),"CardNumber");

            modelBuilder.Entity<BalanceModel>()
                .HasMany(x => x.Detail)
                .WithOne(m => m.Balance)
                .HasForeignKey(m=>m.BalanceId);


            //Not being used by the time, as not requiered for now in the test.
            modelBuilder.Entity<CardHolderModel>()
                .HasKey(x => x.IdNumber);

            modelBuilder.Entity<CardHolderModel>()
                .HasMany(ch => ch.Cards);

        }
        public DbSet<CardModel> CreditCard => Set<CardModel>();
        public DbSet<BalanceModel> Balance => Set<BalanceModel>();
        public DbSet<BalanceDetailModel> BalanceDetail => Set<BalanceDetailModel>();


        public DbSet<CardHolderModel> CardHolders => Set<CardHolderModel>();
    }

}
