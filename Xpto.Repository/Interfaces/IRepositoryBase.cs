using System.Collections.Generic;

namespace Xpto.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T obj);
        T GetById(int id);
        T GetByReferenceCode(string id);
        T GetByReferenceCode(int id);
        IEnumerable<T> GetAll();
        void Update(T obj);
        void Remove(T obj);
        void Dispose();
    }
}
