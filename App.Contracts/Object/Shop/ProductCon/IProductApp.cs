using MyFrameWork.AppTool;

namespace App.Contracts.Object.Shop.ProductCon
{
    public interface IProductApp
    {
        List<ProductView> GetAll();
        OPT Create(ProductView productView);
        OPT DeleteBy(int productid);
    }

}
