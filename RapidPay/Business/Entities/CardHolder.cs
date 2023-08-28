namespace RapidPay.Business.Entities
{
    public class CardHolder
    {
        public string IdNumber { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int Id => throw new NotImplementedException();
    }
}