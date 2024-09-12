using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Respone
{
    public class SupplierByIdResp : GlobalResponse
    {
        public Supplier data { get; set; } = new Supplier();
    }
}
