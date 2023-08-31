using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RapidPay.Business.Entities;
using RapidPay.Data.Model;

namespace RapidPay.Data
{

    public class UnitOfWork : IDisposable
    {
        private DbContext context;
        private GenericRepository<CardModel> cardRepository;
        private GenericRepository<BalanceModel> balanceRepository;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public GenericRepository<CardModel> CardRepository
        {
            get
            {

                if (this.cardRepository == null)
                {
                    this.cardRepository = new GenericRepository<CardModel>(context);
                }
                return cardRepository;
            }
        }

        public GenericRepository<BalanceModel> BalanceRepository
        {
            get
            {

                if (this.balanceRepository == null)
                {
                    this.balanceRepository = new GenericRepository<BalanceModel>(context);
                }
                return balanceRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}