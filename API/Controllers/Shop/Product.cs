using App.Contracts.Object.Shop.ProductCon;
using Microsoft.AspNetCore.Mvc;
using MyFrameWork.AppTool;

namespace API.Controllers.Shop
{

    [ApiController]
    [Route("[controller]")]
    public class Product : ControllerBase
    {
        private readonly IProductApp productApp;

        public Product(IProductApp productApp)
        {
            this.productApp = productApp;
        }


        [HttpPost]
        
        public ActionResult<OPTResult<ProductView>> Index([FromBody] Pagination pagination)
        {
            return productApp.GetAll(pagination);
        }

        [HttpGet]
        [Route("/search")]
        public ActionResult<IEnumerable<ProductView>> search([FromQuery] string? name ,
            [FromQuery] int? minprice , [FromQuery] int? maxprice
            )
        {
            var productSearch = new ProductSearchCriteria()
                                    {
                Name =name ?? null
                ,
                MinPrice = minprice ?? 0
                ,
                MaxPrice=maxprice ?? 0};
            return productApp.SearchProducts(productSearch);
        }



        [HttpPost]
        [Route("/creat")]
        public ActionResult create([FromBody] ProductCreate product)
        {

            var opt = productApp.Create(product);
            if (opt.IsSucceeded) { return Ok(); }
            else { return Ok ( new { warning = opt.Message } ); }

        }

        [HttpDelete]
        [Route("/delete")]
        public OkResult delete(int id)
        {
            productApp.DeleteBy(id);
            return Ok();
        }
    }
}
