using App.Contracts.Object.Shop.ProductCon;
using AutoMapper;
using Domain.Objects.Shop;
using MyFrameWork.AppTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Object.Shop.ProductApp
{
    public class ProductApp : IProductApp
    {
        private readonly IProductRep _productRep;
        private readonly IMapper _mapper;

        public ProductApp(IProductRep productRep , IMapper mapper)
        {
            _productRep = productRep;
            this._mapper = mapper;
        }



        //Create method 
        public OPT Create(ProductCreate productCreate)
        {
            OPT opt = new OPT();
            var codeExist = _productRep.Exist(c=>c.ProductCode == productCreate.ProductCode);
            if (codeExist) { opt.Failed(" . کد محصول تکراریست "); }
            else
            {
                var product = _mapper.Map<Product>(productCreate);
                _productRep.Create(product);
                opt.Succeeded();
                _productRep.SaveChanges();
            }

            return opt;
        }

        public OPT DeleteBy(int productid)
        {
            OPT opt = new OPT();
            _productRep.DeleteByID(productid);
            opt.Succeeded();
            _productRep.SaveChanges();
            return opt;

        }

        public OPTResult<ProductView> GetAll(Pagination pagination)
        {    // دریافت تمام محصولات  
            var products = _productRep.Get();

            // تبدیل داده‌ها به نوع ViewModel  
            var data = _mapper.Map<List<ProductView>>(products);

            // تعداد کل رکوردها  
            var totalRecords = data.Count;

            // انجام صفحه‌بندی با استفاده از متدهای Pagination  
            var pagedData = data
                .Skip(pagination.CalculateSkip())
                .Take(pagination.PageSize)
                .ToList();

            // تعداد کل صفحات  
            var totalPages = pagination.CalculateTotalPages(totalRecords);

            // آماده‌سازی و بازگشت نتیجه  
            return new OPTResult<ProductView>
            {
                IsSucceeded = true,
                Message = "داده با موفقیت بارگذاری شد.",
                Data = pagedData,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

        }


        public List<ProductView> SearchProducts(ProductSearchCriteria criteria)
        {
            Expression<Func<Product, bool>> filter = product => true; 

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                filter = filter.And(product => product.Name.Contains(criteria.Name));   
            }

            if (criteria.MinPrice.HasValue && criteria.MinPrice > 0)
            {
                filter = filter.And(product => product.Price >= criteria.MinPrice.Value);
            }

            if (criteria.MaxPrice.HasValue && criteria.MaxPrice > 0)
            {
                filter = filter.And(product => product.Price <= criteria.MaxPrice.Value);
            }


           var products = _productRep.GetFiltered(filter);
            return _mapper.Map<List<ProductView>>(products);
        }
    }





    public interface IProductRep : IBaseRep<Product, int> { }
}
