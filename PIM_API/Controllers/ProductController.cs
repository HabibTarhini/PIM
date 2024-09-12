using Microsoft.AspNetCore.Mvc;
using PIM.Business.ProductBusiness;
using PIM.Entities.Request;
using PIM.Entities.Respone;

namespace PIM_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet("ById/{id}")]
        public async Task<ProductByIdResp> GetProduct(int id)
        {
            var req = new ProductByIdReq { id = id };
            return await _productBusiness.GetProductById(req);
        }

        [HttpGet("List")]
        public async Task<ProductListResp> GetProducts([FromQuery] ProductListReq req)
        {
            return await _productBusiness.GetProductList(req);
        }

        [HttpPost("Add")]
        public async Task<ProductAddResp> AddProduct([FromBody] ProductAddReq req)
        {
            return await _productBusiness.AddProduct(req);
        }

        [HttpPut("Update/{id}")]
        public async Task<ProductUpdateResp> UpdateProduct(int id, [FromBody] ProductUpdateReq req)
        {
            req.id = id;
            return await _productBusiness.UpdateProduct(req);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ProductDeleteResp> DeleteProduct(int id)
        {
            var req = new ProductDeleteReq { id = id };
            return await _productBusiness.DeleteProduct(req);
        }
    }
}

