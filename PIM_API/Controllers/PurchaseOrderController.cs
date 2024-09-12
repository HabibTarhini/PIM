using Microsoft.AspNetCore.Mvc;
using PIM.Business.PurchaseOrder;
using PIM.Entities.Request;
using PIM.Entities.Respone;

namespace PIM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderBusiness _purchaseOrderBusiness;

        public PurchaseOrderController(IPurchaseOrderBusiness purchaseOrderBusiness)
        {
            _purchaseOrderBusiness = purchaseOrderBusiness;
        }

        [HttpPost("CreatePurchase")]
        public async Task<PurchaseOrderCreateResp> CreatePurchaseOrder([FromBody] PurchaseOrderCreateReq req)
        {
            return await _purchaseOrderBusiness.CreatePurchaseOrder(req);
        }

        [HttpPost("UpdateStatus")]
        public async Task<PurchaseOrderUpdateStatusResp> UpdatePurchaseOrderStatus([FromBody] PurchaseOrderUpdateStatusReq req)
        {
            return await _purchaseOrderBusiness.UpdatePurchaseOrderStatus(req);
        }
    }
}