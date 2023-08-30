namespace RapidPay.Data.Model
{
    public class BalanceDetailModel
    {
        public Guid DetailId { get; set; }
        public Guid BalanceId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public string CurrencyCode { get; set; }
        public BalanceModel Balance { get; set; }

    }
}