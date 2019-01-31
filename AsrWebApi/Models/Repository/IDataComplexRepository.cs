using System.Collections.Generic;

namespace AsrWebApi.Models.Repository
{
    public interface IDataComplexRepository<TEntity, TKey> where TEntity : class where TKey : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey pair);
        TKey Add(TEntity item);
        TKey Update(TKey pair, TEntity item);
        TKey Delete(TKey pair);
    }
}
