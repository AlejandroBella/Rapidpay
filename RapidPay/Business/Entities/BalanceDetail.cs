namespace RapidPay.Business.Entities
{
    public class BalanceDetail
    {
        public Guid DetailId { get; set; }
        public Guid IdBalance { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}