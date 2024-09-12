using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities
{
    public class PurchaseReceiptItem
    {
        public int Id { get; set; }
        public int PurchaseReceiptId { get; set; }
        public int ProductId { get; set; }
        public int ReceivedQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }

        public PurchaseReceipt PurchaseReceipt { get; set; }
        public Product Product { get; set; }
    }
}
