using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities
{
    /* public class PurchaseOrder
     {
         public int Id { get; set; }
         public int SupplierId { get; set; }
         public decimal TotalAmount { get; set; }
         public DateTime Created { get; set; } = DateTime.Now;
         public DateTime? Updated { get; set; }
         public int StatusId { get; set; }

         public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } // Link to items
     }*/
    public class PurchaseOrder
    {
        public int Id { get; set; } // Unique identifier for the order
        public int SupplierId { get; set; } // Foreign key to the Supplier
        public decimal TotalAmount { get; set; } // Total amount of the order
        public DateTime Created { get; set; } = DateTime.Now; // Date when the order was created
        public DateTime? Updated { get; set; } // Date when the order was last updated
        public int StatusId { get; set; }  // Status of the order (1 = Pending, 2 = Completed, 3 = Canceled)
        //public DateTime OrderDate { get; set; } // Date when the order was placed

        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } // Collection of items in the order
    }
}
