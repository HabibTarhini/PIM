using PIM.Entities.Request;
using PIM.Entities.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Business.PurchaseReceiptBusiness
{
    public interface IPurchaseReceiptBusiness
    {
        Task<PurchaseReceiptCreateResp> CreatePurchaseReceipt(PurchaseReceiptCreateReq req);
        Task<PurchaseReceiptViewResp> ViewPurchaseReceipt(PurchaseReceiptViewReq req);
    }
}
