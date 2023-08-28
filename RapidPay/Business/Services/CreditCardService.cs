using AutoMapper;
using RapidPay.Business.Entities;
using RapidPay.Business.Helpers;
using RapidPay.Data.Interfaces;
using RapidPay.Exceptions;
using RapidPay.View.Entities;
using System.Text.RegularExpressions;

namespace RapidPay.Business.Services
{
    public class CreditCardService : DataServiceBase<CardView, string>
    {
        private readonly IRepository<Balance, Guid> balanceRepository;
        private IRepository<Card, string> cardRepository;

        public CreditCardService(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
        {
            cardRepository = serviceProvider.GetRequiredService<IRepository<Card, string>>();
            balanceRepository = serviceProvider.GetRequiredService<IRepository<Balance, Guid>>();
        }

        public override bool Delete(string id)
        {
            try
            {
                if (!ValidateCardNumber(id))
                {
                    throw new ArgumentException();
                }
                cardRepository.Delete(id);
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

        public override IList<View.Entities.CardView> GetAll()
        {
            var dbItem = cardRepository.GetAll();
            var item = MapperService.Map<IList<View.Entities.CardView>>(dbItem);
            return item;
        }

        public override View.Entities.CardView GetById(string id)
        {
            var dbItem = GetCard(id);
            var item = MapperService.Map<View.Entities.CardView>(dbItem);
            return item;
        }

        public override bool Set(View.Entities.CardView item)
        {
            try
            {
                var dbItem = MapperService.Map<Entities.Card>(item);
                cardRepository.Set(dbItem);

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

        public override bool Update(string id, View.Entities.CardView item)
        {
            try
            {
                var dbItem = GetCard(id);
                dbItem = MapperService.Map<Entities.Card>(item);
                cardRepository.Update(id, dbItem);
                return true;
            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....
                return false;
            }
        }


        public bool AddBalance(string id, double amount)
        {
            var item = GetCard(id);
            if (item == null)
                if (item == null)
                {
                    throw new KeyNotFoundException();
                }
            try
            {
                item = MapperService.Map<Entities.Card>(item);
                item.Balance.AddMovement(amount);
                item.Balance.RefreshBalance();
                balanceRepository.Update(item.Balance.BalanceId, item.Balance);
                return true;
            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....
                return false;
            }
        }

        public BalanceView GetBalance(string id)
        {
            var item = GetCard(id);
            if (item == null)
                if (item == null)
                {
                    throw new KeyNotFoundException();
                }

            item = MapperService.Map<Entities.Card>(item);

            return MapperService.Map<BalanceView>(item.Balance);

        }

        private Card GetCard(string id)
        {
            var dbItem = cardRepository.Get(id);
            if (dbItem == null)
            {
                throw new KeyNotFoundException();
            }

            return dbItem;
        }

        public override bool Validate(CardView entity, DataAction action)
        {
            switch (action)
            {
                case DataAction.Create:
                    return ValidateCreation(entity);
                case DataAction.Update:
                    return ValidateModification(entity);
                case DataAction.Delete:
                    return ValidateDelete(entity);
                default:
                    throw new InvalidOperationException();
            }

        }

        protected override bool ValidateDelete(CardView entity)
        {
            if (entity == null)
                return false;

            if (!ValidateCardNumber(entity.Number))
                return false;

            return true;
        }

        protected override bool ValidateModification(CardView entity)
        {
            if (!ValidateCreation(entity))
                return false;

            return true;
        }

        protected override bool ValidateCreation(CardView entity)
        {
            if (entity == null)
                return false;

            if (!ValidateCardNumber(entity.Number))
                return false;

            if (entity.PIN <= 0)
                return false;

            if (string.IsNullOrEmpty(entity.HolderIdNumber))
                return false;

            if (entity.DueDate < DateTime.Now.Date.AddYears(1))
                return false;

            return true;

        }

        private bool ValidateCardNumber(string number)
        {
            var cardCheck = new Regex(@"^(1298|1267|4444|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");

            return cardCheck.IsMatch(number);
        }
    }
}
