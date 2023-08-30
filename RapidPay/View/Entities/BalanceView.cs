namespace RapidPay.View.Entities;

public class BalanceView
{
    public Guid BalanceId { get; set; }
    public double CurrentBalance { get; set; }
    public DateTime LastOperation { get; set; }
}
