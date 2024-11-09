using App.Contracts.Object.Shop.ProductCon;
using Domain.Objects.Shop;
using MyFrameWork.AppTool;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public OPT Create(ProductView productView)
        {
            OPT opt = new OPT();
            var product = new Product
            {
                Name = productView.Name,
                Price = productView.Price,
                CountType = productView.CountType,
            };
            _productRep.Create(product);
            opt.Succeeded();



            return opt;
        }

        public OPT DeleteBy(int productid)
        {
            OPT opt = new OPT();
            _productRep.DeleteByID(productid);
            opt.Succeeded();
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
                CountType= c.CountType,

            }).ToList();
        }
    }
    public interface IProductRep : IBaseRep<Product, int> { }
}
