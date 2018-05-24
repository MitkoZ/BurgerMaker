using DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        List<T> GetAll(Func<T, bool> filter = null);

        void Save(T item);

        void Create(T item);

        void Update(T item, Func<T, bool> findByIDPredecate);
    }
}
