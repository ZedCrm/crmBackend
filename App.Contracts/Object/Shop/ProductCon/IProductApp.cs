using MyFrameWork.AppTool;

namespace App.Contracts.Object.Shop.ProductCon
{
    public interface IProductApp
    {
        public List<ProductView> SearchProducts(ProductSearchCriteria criteria);
        List<ProductView> GetAll();
        OPT Create(ProductCreate productCreate);
        OPT DeleteBy(int productid);
    }

}
