using AutoMapper;
using RapidPay.Business.Entities;
using RapidPay.Data.Model;
using RapidPay.Data.Interfaces;

namespace RapidPay.Data.Repositories
{
    public class CreditCardRepository : IRepository<Card, string>
    {
        private readonly Database db;
        private readonly IMapper _mapper;

        public CreditCardRepository(IConfiguration configuration, IMapper mapper)
        {
            db = new Database(configuration);
            _mapper = mapper;
        }
        public void Set(Card item)
        {
            var card = _mapper.Map<CardModel>(item);
            card.Active = true;
#if DEBUG
            if (string.IsNullOrEmpty(card.UserId))
                card.UserId = "System";
#endif
            db.CreditCard.Add(card);
            db.SaveChangesAsync();
        }
        public Card Get(string id)
        {
            var card = db.Find<CardModel>(id);
            if (card == null)
                throw new KeyNotFoundException();

            return _mapper.Map<Card>(card);
        }

        public void Delete(string id)
        {
            var card = db.Find<CardModel>(id);
            if (card == null)
                throw new KeyNotFoundException();

            db.Remove(id);
            db.SaveChangesAsync();
        }

        public void Update(string id, Card item)
        {

            var card = db.Find<CardModel>(id);

            if (card == null)
                throw new KeyNotFoundException();

            db.Update(card);
            card = _mapper.Map<CardModel>(item);

            db.SaveChangesAsync();
        }

        public List<Card> GetAll()
        {
            var list = db.CreditCard.Where(x => x.Active).ToList();
            return _mapper.Map<List<Card>>(list);
        }

    }
}
