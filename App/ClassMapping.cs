using App.Contracts.Object.Shop.ProductCon;
using AutoMapper;
using Domain.Objects.Shop;

namespace App
{
    public class ClassMapping : Profile
    {

        public ClassMapping()
        {
            CreateMap<Product, ProductView>();
            CreateMap<ProductCreate , Product>();
        }
    }
}
