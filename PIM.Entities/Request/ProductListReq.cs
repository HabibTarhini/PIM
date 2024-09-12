using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Request
{
    public class ProductListReq
    {
        public int PageSize { get; set; } // Number of items per page
        public int PageNumber { get; set; } // Current page number
        public string Search { get; set; } // Optional search keyword for product name
    }
}
