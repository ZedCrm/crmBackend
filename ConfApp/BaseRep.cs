using App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConfApp
{
    public abstract class BaseRep<T, TKey> : IBaseRep<T, TKey> where T : class
    {
        private readonly MyContext _ctx;
        public BaseRep(MyContext ctx)
        {
            _ctx = ctx;
        }
        public void Create(T entity)
        {
            _ctx.Add(entity);
        }

        public void Delete(T entity)
        {
            _ctx.Remove(entity);
        }

        public void DeleteByID(TKey id)
        {
            var TforDelete = this.Get(id);
            this.Delete(TforDelete);
        }

        public bool Exist(Expression<Func<T, bool>> expression)
        {
            return _ctx.Set<T>().Any(expression);
        }

        public T Get(TKey id)
        {

            return _ctx.Find<T>(id);
        }

        public List<T> Get()
        {
            return _ctx.Set<T>().ToList();
        }

        public  List<T> GetFiltered(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _ctx.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return  query.ToList();
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
