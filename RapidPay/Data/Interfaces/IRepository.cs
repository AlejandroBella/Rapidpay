namespace RapidPay.Data.Interfaces
{
    public interface IRepository<T,I>
    {
        T Get(I id);
        void Set(T item);
        List<T> GetAll();
        void Delete(I id);
        void Update(I id, T item);
    }
}
