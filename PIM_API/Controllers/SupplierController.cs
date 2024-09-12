using Microsoft.AspNetCore.Mvc;
using PIM.Business.Supplier;
using PIM.Entities;
using PIM.Entities.Request;
using PIM.Entities.Respone;

namespace PIM_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierBusiness _supplierBusiness;

        public SupplierController(ISupplierBusiness supplierBusiness)
        {
            _supplierBusiness = supplierBusiness;
        }

        [HttpPost("Add")]
        public async Task<SupplierAddResp> AddSupplier([FromBody] SupplierAddReq req)
        {
            return await _supplierBusiness.AddSupplier(req);
        }

        [HttpPost("Update")]
        public async Task<SupplierUpdateResp> UpdateSupplier([FromBody] SupplierUpdateReq supplier)
        {
            return await _supplierBusiness.UpdateSupplier(supplier);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<SupplierDeleteResp> DeleteSupplier(int id)
        {
            var req = new SupplierDeleteReq { Id = id };
            return await _supplierBusiness.DeleteSupplier(req);
        }

        [HttpGet("ById/{id}")]
        public async Task<SupplierByIdResp> GetSupplier(int id)
        {
            var req = new SupplierByIdReq { Id = id };
            return await _supplierBusiness.GetSupplierById(req);
        }
    }
}
