namespace RapidPay.Data.Model
{
    public class CardModel
    {
        public bool Active { get; set; }

        public string Number { get; set; }

        public int PIN { get; set; }

        public string HolderIdNumber { get; set; }

        public Guid BalanceId { get; set; }

        public BalanceModel Balance { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public string UserId { get; set; }
    }
}
