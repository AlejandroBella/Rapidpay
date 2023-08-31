using RapidPay.Business.Interfaces;

namespace RapidPay.Business.Entities
{
    public class Card:IBusinessEntity<string>
    {
        public Card()
        {
            Balance = new Balance();            
        }
        public string Id => Number;
        public string Number { get; set; }

        public int PIN { get; set; }

        public string HolderIdNumber { get; set; }

        public double CurrentBalance { get; set; }

        public Balance Balance { get; set; }

        public DateTime DueDate { get; set; }
    }
}
