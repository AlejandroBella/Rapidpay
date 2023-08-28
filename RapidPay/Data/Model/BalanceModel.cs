namespace RapidPay.Data.Model
{
    public class BalanceModel
    {
        public Guid BalanceId { get; set; }
        public string CardNumber { get; set; }
        public string CurrencyCode { get; set; }
        public double CurrentBalance { get; set; }
        public CardModel Card { get; set; }
        public List<BalanceDetailModel> Detail { get; set; }
    }
}
