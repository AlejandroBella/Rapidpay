

using RapidPay.Business.Interfaces;

namespace RapidPay.Business.Entities
{
    public class Balance : IBusinessEntity<Guid>
    {
        public Balance()
        {
            Detail = new List<BalanceDetail>();
        }

        public Guid BalanceId { get; set; }
        public string CardNumber { get; set; }
        public List<BalanceDetail> Detail { get; set; }
        public double CurrentBalance { get; set; }

        public Guid Id {
            get
            {
                return BalanceId;
            }
        }
    }
}
