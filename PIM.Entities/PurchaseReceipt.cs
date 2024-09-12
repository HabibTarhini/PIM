using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities
{
    public class PurchaseReceipt
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
        public ICollection<PurchaseReceiptItem> PurchaseReceiptItems { get; set; }
    }
    //public class PurchaseReceipt
    //{
    //    public int Id { get; set; }
    //    public int PurchaseOrderId { get; set; }
    //    public DateTime ReceiptDate { get; set; } = DateTime.Now;
    //    public int TotalItems { get; set; }
    //    public decimal TotalPrice { get; set; }
    //    public DateTime Created { get; set; } = DateTime.Now;
    //    public DateTime? Updated { get; set; }

    //    public PurchaseOrder PurchaseOrder { get; set; }
    //    public ICollection<PurchaseReceiptItem> PurchaseReceiptItems { get; set; }
    //}
}
