using AutoMapper;
using RapidPay.Business.Entities;
using RapidPay.Data.Interfaces;

namespace RapidPay.Data.Repositories
{
    public class CardHolderRepository : IRepository<CardHolder, string>
    {
        private readonly Database db;
        private readonly IMapper _mapper;

        public CardHolderRepository(IConfiguration configuration, IMapper mapper)
        {
            db = new Database(configuration);
            _mapper = mapper;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public CardHolder Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<CardHolder> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Set(CardHolder item)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, CardHolder item)
        {
            throw new NotImplementedException();
        }
    }
}
