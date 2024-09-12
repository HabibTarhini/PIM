using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Request
{
    public class PurchaseOrderUpdateStatusReq
    {
        public int OrderId { get; set; }   // Purchase order ID
        public int StatusId { get; set; }  // Status ID (e.g., 1 for Pending, 2 for Completed, 3 for Cancelled)
    }
}
