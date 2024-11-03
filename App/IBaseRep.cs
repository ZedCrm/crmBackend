using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public interface IBaseRep<T, TKey> where T : class
    {
        T Get(TKey id);
        List<T> Get();
        void Create(T entity);
        void Delete(T entity);
        void DeleteByID(TKey id);
        bool Exist(Expression<Func<T, bool>> expression);
        void SaveChanges();
    }
}
