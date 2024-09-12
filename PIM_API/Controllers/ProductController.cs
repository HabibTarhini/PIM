using Microsoft.AspNetCore.Mvc;
using PIM.Business.ProductBusiness;
using PIM.Entities.Request;
using PIM.Entities.Respone;

namespace PIM_API.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductBusiness _productBusiness;

    public ProductController(IProductBusiness productBusiness)
    {
        _productBusiness = productBusiness;
    }

    [HttpGet("{id}")]
    public async Task<ProductByIdResp> GetProduct(int id)
    {
        var req = new ProductByIdReq { id = id };
        return await _productBusiness.GetProductById(req);
    }

    [HttpGet("list")]
    public async Task<ProductListResp> GetProducts([FromQuery] ProductListReq req)
    {
        return await _productBusiness.GetProductList(req);
    }

    [HttpPost]
    public async Task<ProductAddResp> AddProduct([FromBody] ProductAddReq req)
    {
        return await _productBusiness.AddProduct(req);
    }

    [HttpPut("{id}")]
    public async Task<ProductUpdateResp> UpdateProduct(int id, [FromBody] ProductUpdateReq req)
    {
        req.id = id;
        return await _productBusiness.UpdateProduct(req);
    }

    [HttpDelete("{id}")]
    public async Task<ProductDeleteResp> DeleteProduct(int id)
    {
        var req = new ProductDeleteReq { id = id };
        return await _productBusiness.DeleteProduct(req);
    }
}

