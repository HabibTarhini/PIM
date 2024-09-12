using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Respone
{
    public class PurchaseReceiptViewResp
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int ReceiptId { get; set; }
        public int PurchaseOrderId { get; set; }
        public List<PurchaseReceiptItemViewResp> Items { get; set; }
    }
    public class PurchaseReceiptItemViewResp
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ReceivedQuantity { get; set; }
    }
}
