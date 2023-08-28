namespace RapidPay.Business.Entities
{
    public class Balance
    {
        public Balance()
        {
            Detail = new List<BalanceDetail>();
        }

        public Guid BalanceId { get; set; }
        public string CardNumber { get; set; }
        public string CurrencyCode { get; set; }
        public List<BalanceDetail> Detail { get; set; }
        public double CurrentBalance { get; set; }
      
}
