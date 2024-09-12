using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; } // Unique identifier for the order item
        public int PurchaseOrderId { get; set; }  // Foreign key to PurchaseOrder
        public PurchaseOrder PurchaseOrder { get; set; } // Navigation property to PurchaseOrder
        public int ProductId { get; set; }  // Foreign key to Product
        public int Quantity { get; set; } // Quantity of the product
        public decimal UnitPrice { get; set; } // Price per unit of the product at the time of order
        public decimal Price { get; set; }  // Total price for this item (Quantity * UnitPrice)
    }
}
