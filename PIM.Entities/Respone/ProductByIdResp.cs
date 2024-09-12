using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Respone
{
    public class ProductByIdResp : GlobalResponse
    {
        public ProductObj data { get; set; } = new ProductObj();
    }
}
