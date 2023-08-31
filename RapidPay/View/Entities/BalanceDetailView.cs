namespace RapidPay.View.Entities
{
    public class BalanceDetailView
    {
        public Guid DetailId { get; set; }
        public DateTime Date { get; set; }
        public string CurrencyCode { get; set; }
        public double Amount { get; set; }
    }
}
