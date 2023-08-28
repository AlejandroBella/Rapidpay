using AutoMapper;
using RapidPay.Business.Entities;
using RapidPay.Business.Helpers;
using RapidPay.Data.Interfaces;
using RapidPay.View.Entities;

namespace RapidPay.Business.Services
{
    public abstract class DataServiceBase<T, I>
    {
        protected readonly IMapper MapperService;
        protected IServiceProvider ServiceProvider;

        public DataServiceBase(IServiceProvider serviceProvider, IMapper mapper)
        {
            ServiceProvider = serviceProvider;
            MapperService = mapper;
        }

        protected DataServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public abstract bool Delete(I id);

        public abstract IList<T> GetAll();

        public abstract T GetById(I id);

        public abstract bool Set(T item);

        public abstract bool Update(I id, T item);
        public abstract bool Validate(T entity, DataAction action);

        protected abstract bool ValidateCreation(T entity);

        protected abstract bool ValidateDelete(T entity);

        protected abstract bool ValidateModification(T entity);
    }
}