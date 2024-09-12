using PIM.Entities;
using PIM.Entities.Request;
using PIM.Entities.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Business.Supplier
{
    public interface ISupplierBusiness
    {
        Task<SupplierAddResp> AddSupplier(SupplierAddReq req);
        Task<SupplierUpdateResp> UpdateSupplier(SupplierUpdateReq req);
        Task<SupplierDeleteResp> DeleteSupplier(SupplierDeleteReq req);
        Task<SupplierByIdResp> GetSupplierById(SupplierByIdReq req);
    }
}
