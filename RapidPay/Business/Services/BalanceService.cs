using AutoMapper;
using RapidPay.Business.Helpers;
using RapidPay.Data.Interfaces;
using RapidPay.Business.Entities;
using RapidPay.View.Entities;
using RapidPay.Exceptions;

namespace RapidPay.Business.Services
{
    public class BalanceService : DataServiceBase<BalanceView, Guid>
    {
        IRepository<Balance, Guid> balanceRepository;
        public BalanceService(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
        {
            balanceRepository = serviceProvider.GetRequiredService<IRepository<Balance, Guid>>();
        }

        // In this schema this should be used by he card service and is not exposed directly to the API
        public override bool Delete(Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    throw new ArgumentException();
                }
                balanceRepository.Delete(id);
                return true;
            }
            catch (KeyNotFoundException notFoundEx)
            {
                //Log the error. and continues
                throw notFoundEx;
            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....
                return false;
            }
        }

        //This does not make sense to implement, withou predicate.
        public override IList<BalanceView> GetAll()
        {
            throw new NotImplementedException();
        }

        public override BalanceView GetById(Guid id)
        {
            var dbItem = balanceRepository.Get(id);
            if (dbItem == null)
            {
                throw new KeyNotFoundException();
            }
            return MapperService.Map<BalanceView>(dbItem);
        }

        // In this schema this should be used by he card service and is not exposed directly to the API
        public override bool Set(BalanceView item)
        {
            try
            {
                var dbItem = MapperService.Map<Entities.Balance>(item);
                balanceRepository.Set(dbItem);

                return true;
            }
            catch (DuplicatedItemException diEx)
            {
                //Log the error. and continues
                throw diEx;

            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....
                return false;
            }
        }
        public bool AddMovement(Guid id, double amount)
        {
            if (amount == 0)
                return false;

            var dbItem = balanceRepository.Get(id);
            if (dbItem == null)
            {
                throw new KeyNotFoundException();
            }

            dbItem.Detail.Add(new BalanceDetail
            {
                Amount = amount,
                Date = DateTime.Now,
                IdBalance = dbItem.BalanceId
            });

            RefreshBalance(dbItem);

            balanceRepository.Update(id, dbItem);
            return true;
        }

        public bool RemoveMovement(Guid id, double amount)
        {
            if (amount == 0)
                return false;

            var dbItem = balanceRepository.Get(id);
            if (dbItem == null)
            {
                throw new KeyNotFoundException();
            }

          var item = dbItem.CurrentBalance.

            RefreshBalance(dbItem);

            balanceRepository.Update(id, dbItem);
            return true;

            return true;
        }

        public override bool Update(Guid id, BalanceView item)
        {
            var dbItem = balanceRepository.Get(id);
            if (dbItem == null)
            {
                throw new KeyNotFoundException();
            }
            dbItem.CurrentBalance = item.CurrentBalance;

            return true;
        }

        private void RefreshBalance(Balance balance)
        {
            balance.CurrentBalance = balance.Detail.Sum(x => x.Amount);
        }

        public double BalanceToDate(Guid id, DateTime date)
        {
            var dbItem = balanceRepository.Get(id);
            if (dbItem == null)
            {
                throw new KeyNotFoundException();
            }
            return dbItem.Detail.Where(c => c.Date <= date).Sum(x => x.Amount);
        }

        public override bool Validate(BalanceView entity, DataAction action)
        {
            throw new NotImplementedException();
        }

        protected override bool ValidateCreation(BalanceView entity)
        {
            throw new NotImplementedException();
        }

        protected override bool ValidateDelete(BalanceView entity)
        {
            throw new NotImplementedException();
        }

        protected override bool ValidateModification(BalanceView entity)
        {
            throw new NotImplementedException();
        }


    }
}
