using PIM.Entities.Request;
using PIM.Entities.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Business.PurchaseOrder
{
    public interface IPurchaseOrderBusiness
    {
        Task<PurchaseOrderCreateResp> CreatePurchaseOrder(PurchaseOrderCreateReq req);
        Task<PurchaseOrderUpdateStatusResp> UpdatePurchaseOrderStatus(PurchaseOrderUpdateStatusReq req);


    }
}
