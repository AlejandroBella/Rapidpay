namespace RapidPay.View.Entities
{
    public class CardView
    {
        public string Number { get; set; }
        
        public bool Active { get; set; }

        public int PIN { get; set; }

        public BalanceView Balance { get; set; }
        
        public DateTime DueDate { get; set; }

        public string HolderIdNumber { get; set; }
    }
}