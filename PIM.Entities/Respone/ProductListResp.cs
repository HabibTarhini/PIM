using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Respone
{
    public class ProductListResp : GlobalResponse
    {
        public List<ProductObj> list { get; set; } = new List<ProductObj>();
        public int totalCount { get; set; }
    }

    public class ProductObj
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}
