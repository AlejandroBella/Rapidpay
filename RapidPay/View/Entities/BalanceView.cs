namespace RapidPay.View.Entities;

public class BalanceView
{
    public Guid BalanceId { get; set; }
    public string CurrencyCode { get; set; }
    public double CurrentBalance { get; set; }

    public DateTime LastOperation { get; set; }
}
