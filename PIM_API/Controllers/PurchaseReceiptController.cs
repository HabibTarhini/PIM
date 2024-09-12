using Microsoft.AspNetCore.Mvc;
using PIM.Business.PurchaseReceiptBusiness;
using PIM.Entities.Request;
using PIM.Entities.Respone;

namespace PIM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseReceiptController : ControllerBase
    {
        private readonly IPurchaseReceiptBusiness _purchaseReceiptBusiness;

        public PurchaseReceiptController(IPurchaseReceiptBusiness purchaseReceiptBusiness)
        {
            _purchaseReceiptBusiness = purchaseReceiptBusiness;
        }

        [HttpPost]
        public async Task<PurchaseReceiptCreateResp> CreatePurchaseReceipt([FromBody] PurchaseReceiptCreateReq req)
        {
            return await _purchaseReceiptBusiness.CreatePurchaseReceipt(req);
        }

        [HttpGet("{id}")]
        public async Task<PurchaseReceiptViewResp> ViewPurchaseReceipt(int id)
        {
            var req = new PurchaseReceiptViewReq { ReceiptId = id };
            return await _purchaseReceiptBusiness.ViewPurchaseReceipt(req);
        }
    }
}

