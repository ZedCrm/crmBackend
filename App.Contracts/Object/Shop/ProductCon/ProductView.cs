using MyFrameWork.AppTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace App.Contracts.Object.Shop.ProductCon
{
    public class ProductView
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        

    }
    public class ProductCreate : ProductView
    {
        public int CountType { get; set; }

    }
    public class ProductSearchCriteria : Pagination
    {
        public string? Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }

}
