using PIM.Entities.Request;
using PIM.Entities.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Business.ProductBusiness
{
    public interface IProductBusiness
    {
        Task<ProductAddResp> AddProduct(ProductAddReq req);
        Task<ProductUpdateResp> UpdateProduct(ProductUpdateReq req);
        Task<ProductDeleteResp> DeleteProduct(ProductDeleteReq req);
        Task<ProductListResp> GetProductList(ProductListReq req);
        Task<ProductByIdResp> GetProductById(ProductByIdReq req);




    }
}
