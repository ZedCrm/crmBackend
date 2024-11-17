using App.Contracts.Object.Shop.ProductCon;
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

        public ProductApp(IProductRep productRep)
        {
            _productRep = productRep;
        }
        public OPT Create(ProductCreate productCreate)
        {
            OPT opt = new OPT();
            var codeExist = _productRep.Exist(c=>c.ProductCode == productCreate.ProductCode);
            if (codeExist) { opt.Failed(" . کد محصول تکراریست "); }
            else
            {
                var product = new Product
                {
                    Name = productCreate.Name,
                    Price = productCreate.Price,
                    CountType = productCreate.CountType,
                    ProductCode = productCreate.ProductCode,
                };
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

        public List<ProductView> GetAll()
        {
            var products = _productRep.Get();
            return products.Select(c=> new ProductView
            {
                Id= c.Id,
                Name= c.Name,
                Price= c.Price,
                ProductCode=c.ProductCode,

            }).ToList();
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
            return products.Select(c => new ProductView
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                ProductCode = c.ProductCode,

            }).ToList();
        }
    }
    public interface IProductRep : IBaseRep<Product, int> { }
}
