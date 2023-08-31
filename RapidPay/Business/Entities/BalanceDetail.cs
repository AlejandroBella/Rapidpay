using RapidPay.Business.Interfaces;

namespace RapidPay.Business.Entities
{
    public class BalanceDetail : IBusinessEntity<Guid>
    {
        public Guid DetailId { get; set; }
        public Guid IdBalance { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }

        public Guid Id
        {
            get
            {
                return DetailId;
            }
        }
    }
}