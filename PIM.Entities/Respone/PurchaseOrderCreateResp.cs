using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Respone
{
    public class PurchaseOrderCreateResp : GlobalResponse
    {
        public int OrderId { get; set; }  // Newly created purchase order ID
        public decimal TotalAmount { get; set; }  // Total amount for the order
    }
}
