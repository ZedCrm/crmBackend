using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.Object.Shop.ProductCon
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int CountType { get; set; }
    }

}
