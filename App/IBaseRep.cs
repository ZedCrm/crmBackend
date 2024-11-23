using App.Contracts.Object.Shop.ProductCon;
using MyFrameWork.AppTool;
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
        List<T>  GetFiltered(Expression<Func<T, bool>> filter = null , ProductSearchCriteria criteria = null);
        List<T> Get();
        List<T> Get(Pagination pagination );
        void Create(T entity);
        void Delete(T entity);
        void DeleteByID(TKey id);
        int Count();
        bool Exist(Expression<Func<T, bool>> expression  );
        void SaveChanges();
    }
}
