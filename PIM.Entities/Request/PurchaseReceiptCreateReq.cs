using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Request
{
    public class PurchaseReceiptCreateReq
    {
        public int PurchaseOrderId { get; set; }
        public List<PurchaseReceiptItemCreateReq> Items { get; set; }
    }

    public class PurchaseReceiptItemCreateReq
    {
        public int ProductId { get; set; }
        public int ReceivedQuantity { get; set; }
    }
}
