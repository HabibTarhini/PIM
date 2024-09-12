using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Request
{
    public class PurchaseOrderCreateReq
    {
        public int SupplierId { get; set; } // Supplier identifier
        public List<PurchaseOrderItemCreateReq> Items { get; set; } // List of order items with product IDs and quantities
    }
    public class PurchaseOrderItemCreateReq
    {
        public int ProductId { get; set; } // Product identifier
        public int Quantity { get; set; } // Quantity of the product
        //public decimal UnitPrice { get; set; } // Price per unit of the product at the time of order
    }
}
