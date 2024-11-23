using App;
using App.Contracts.Object.Shop.ProductCon;
using Microsoft.EntityFrameworkCore;
using MyFrameWork.AppTool;
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

        public int Count()
        {
            return _ctx.Set<T>().Count();  
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

        public List<T> Get(Pagination pagination)
        {
            var query = _ctx.Set<T>().AsQueryable();

            // Sorting  
            if (!string.IsNullOrEmpty(pagination.SortBy))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, pagination.SortBy);
                var orderByExpression = Expression.Lambda(property, parameter);

                // Use dynamic methods to infer type arguments  
                var methodName = pagination.SortDirection ? "OrderBy" : "OrderByDescending";
                var method = typeof(Queryable)
                    .GetMethods()
                    .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), property.Type);

                // Call the method dynamically  
                query = (IQueryable<T>)method.Invoke(null, new object[] { query, orderByExpression });
            }

            // Apply pagination  
            return query.Skip(pagination.CalculateSkip())
                        .Take(pagination.PageSize)
                        .ToList();
        }







        public List<T> GetFiltered(Expression<Func<T, bool>> filter = null, ProductSearchCriteria pagination = null)
        {

            IQueryable<T> query = _ctx.Set<T>();

            // Apply filter if provided  
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply sorting if pagination is provided  
            if (pagination != null)
            {
                // Create an expression for sorting  
                if (!string.IsNullOrEmpty(pagination.SortBy))
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, pagination.SortBy);
                    var orderByExpression = Expression.Lambda(property, parameter);

                    // Select the appropriate OrderBy or OrderByDescending method  
                    var methodName = pagination.SortDirection ? "OrderBy" : "OrderByDescending";
                    var method = typeof(Queryable)
                        .GetMethods()
                        .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), property.Type);

                    // Call the method dynamically  
                    query = (IQueryable<T>)method.Invoke(null, new object[] { query, orderByExpression });
                }

                // Apply pagination  
                query = query.Skip(pagination.CalculateSkip())
                             .Take(pagination.PageSize);
            }

            return query.ToList();
        }






        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
