using System.Collections.Generic;

namespace AspNetCore3_WebAPI_JWT.Interfaces
{
    public interface IRepository<TEntity>
    {
        public TEntity SelectById(int id);

        public IEnumerable<TEntity> SelectAll();

        public bool Insert(TEntity entity);

        public bool Update(TEntity entity);

        public bool Delete(TEntity entity);

        public bool DeleteById(int id);
    }
}
