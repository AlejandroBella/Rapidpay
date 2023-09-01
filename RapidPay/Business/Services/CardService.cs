using AutoMapper;
using RapidPay.Business.Entities;
using RapidPay.Data.Model;
using RapidPay.Exceptions;
using RapidPay.View.Entities;
using System.Text.RegularExpressions;

namespace RapidPay.Business.Services
{
    public class CardService : DataServiceBase<Card, string>
    {
        public CardService(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
        {
        }

        public override bool Delete(string id)
        {
            try
            { 
                if (!ValidateCardNumber(id))
                {
                    throw new ArgumentException();
                }
                var item = unitOfWork.CardRepository.GetByID(id);

                if (item == null)
                    throw new KeyNotFoundException();

                unitOfWork.CardRepository.Delete(item);
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....
                throw;
            }
        }

        public override IList<Card> GetAll()
        {
            var dbItem = unitOfWork.CardRepository.Get();
            var item = MapperService.Map<IList<Card>>(dbItem);
            return item;
        }

        public override Card GetById(string id)
        {
            var dbItem = GetCard(id);
            var result = MapperService.Map<Card>(dbItem);

            return result;
        }

        public override bool Create(Card item)
        {
            try
            {
                if (!ValidateCardNumber(item.Number))
                {
                    throw new ArgumentException();
                }

                if (unitOfWork.CardRepository.GetByID(item.Number) != null)
                    throw new DuplicatedItemException();

                var cardDb = MapperService.Map<CardModel>(item);
#if DEBUG
                if (string.IsNullOrEmpty(cardDb.UserId))
                    cardDb.UserId = "System";
#endif
                cardDb.LastUpdate = DateTime.Now;
                cardDb.Active = true;
                cardDb.Balance = new BalanceModel
                {
                    CardNumber = item.Number,
                    CurrentBalance = 0
                };
                unitOfWork.CardRepository.Insert(cardDb);
                unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....
                throw;
            }
        }

        public override bool Update(string id, Card item)
        {
            try
            {
                if (!ValidateCardNumber(id))
                {
                    throw new ArgumentException();
                }
                var modelItem = GetCard(id);

                var dbItem = MapperService.Map<CardModel>(modelItem);
                unitOfWork.CardRepository.Update(dbItem);
                unitOfWork.Save();
                return true;

            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....

                throw;
            }
        }


        public bool AddBalance(BalanceDetail detail)
        {            
            
            var card = unitOfWork.CardRepository.Get(x=>x.BalanceId == detail.BalanceId).FirstOrDefault();
            if(card == null)
            {
                throw new KeyNotFoundException();                
            }
            try
            {

                card.CurrenBalance += detail.Amount;
                card.Balance.Detail.Add(
                    new BalanceDetailModel
                    {
                        Amount = detail.Amount,
                        CurrencyCode = "",
                        Date = DateTime.Now,
                        BalanceId = card.Balance.BalanceId
                    });

                unitOfWork.CardRepository.Update(MapperService.Map<CardModel>(card));
                unitOfWork.Save();
                
                return true;
            }
            catch (Exception ex)
            {
                //Exception is being logged in some repository
                //....
                return false;
            }
        }

        public Balance GetBalance(string id)
        {
            var item = GetCard(id);
            if (item == null)
                if (item == null)
                {
                    throw new KeyNotFoundException();
                }

            item = MapperService.Map<Entities.Card>(item);

            return MapperService.Map<Balance>(item.Balance);

        }

        private Card GetCard(string id)
        {
            var dbItem = unitOfWork.CardRepository.GetByID(id);
            if (dbItem == null)
            {
                throw new KeyNotFoundException();
            }

            return MapperService.Map<Card>(dbItem);
        }


        protected override bool ValidateDelete(Card entity)
        {
            if (entity == null)
                return false;

            if (!ValidateCardNumber(entity.Number))
                return false;

            return true;
        }

        protected override bool ValidateModification(Card entity)
        {
            if (!ValidateCreation(entity))
                return false;

            return true;
        }

        protected override bool ValidateCreation(Card entity)
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
