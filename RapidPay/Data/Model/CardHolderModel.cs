using RapidPay.Data.Interfaces;
namespace RapidPay.Data.Model
{
    public class CardHolderModel : IModel<string>
    {
        public string Id
        {
            get => IdNumber;
        }
        public bool Active { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<CardModel> Cards { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly CreationDate { get; set; }
        public DateOnly LastUpdate { get; set; }
        public string UserId { get; set; }
    }
}