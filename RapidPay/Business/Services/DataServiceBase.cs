using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RapidPay.Business.Helpers;
using RapidPay.Data;

namespace RapidPay.Business.Services
{
    public abstract class DataServiceBase<T, I>
    {
        protected readonly IMapper MapperService;
        protected IServiceProvider ServiceProvider;
        protected UnitOfWork unitOfWork;

        public DataServiceBase(IServiceProvider serviceProvider, IMapper mapper)
        {
            ServiceProvider = serviceProvider;
            MapperService = mapper;
            unitOfWork = new UnitOfWork(serviceProvider.GetRequiredService<DbContext>());
        }

        protected DataServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public abstract bool Delete(I id);

        public abstract IList<T> GetAll();

        public abstract T GetById(I id);

        public abstract bool Create(T item);

        public abstract bool Update(I id, T item);

        public virtual bool Validate(T entity, DataAction action)
        {
            switch (action)
            {
                case DataAction.Create:
                    return ValidateCreation(entity);
                case DataAction.Update:
                    return ValidateModification(entity);
                case DataAction.Delete:
                    return ValidateDelete(entity);
                default:
                    throw new InvalidOperationException();
            }

        }

        protected abstract bool ValidateCreation(T entity);

        protected abstract bool ValidateDelete(T entity);

        protected abstract bool ValidateModification(T entity);
    }
}