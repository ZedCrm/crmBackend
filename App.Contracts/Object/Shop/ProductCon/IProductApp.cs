using MyFrameWork.AppTool;


namespace App.Contracts.Object.Shop.ProductCon
{
    public interface IProductApp
    {
        public List<ProductView> SearchProducts(ProductSearchCriteria criteria);
        OPTResult<ProductView> GetAll(Pagination pagination);
        OPT Create(ProductCreate productCreate);
        OPT DeleteBy(int productid);
    }

}
