namespace RapidPay.Data.Interfaces
{
    public interface IModel<I>
    {
        public I Id { get; }

        public bool Active{ get; set; }
    }
}
