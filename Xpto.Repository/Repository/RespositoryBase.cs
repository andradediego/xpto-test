using System;
using System.Collections.Generic;
using System.Linq;
using Xpto.Data;
using System.Data.Entity;

namespace Xpto.Repository
{
    public class RespositoryBase<T> : IDisposable,
        IRepositoryBase<T> where T : class, IEntities, new()
    {
        protected XptoDbContext db = new XptoDbContext();

        public void Add(T obj)
        {
            db.Set<T>().Add(obj);
            db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public T GetByReferenceCode(string id)
        {
            int _id = 0;
            Int32.TryParse(string.Format("0{0}", id), out _id);
            return GetByReferenceCode(_id);
        }

        public T GetByReferenceCode(int id)
        {
            return db.Set<T>().FirstOrDefault(x => x.CodReferencia == id);
        }

        public void Remove(T obj)
        {
            db.Set<T>().Remove(obj);
            db.SaveChanges();
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
