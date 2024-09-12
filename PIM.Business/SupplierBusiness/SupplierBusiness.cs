using Microsoft.EntityFrameworkCore;
using PIM.Entities;
using PIM.Entities.Request;
using PIM.Entities.Respone;
using PIM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PIM.Business.Supplier.SupplierBusiness;

namespace PIM.Business.Supplier
{
    public class SupplierBusiness : ISupplierBusiness
    {
        private readonly ApplicationDbContext _context;

        public SupplierBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SupplierAddResp> AddSupplier(SupplierAddReq req)
        {
            bool emailExists = await _context.Suppliers.AnyAsync(s => s.Email == req.Email);
            bool phoneExists = await _context.Suppliers.AnyAsync(s => s.Phone == req.Phone);

            if (emailExists)
            {
                return new SupplierAddResp
                {
                    statusCode = 1,
                    message = "A supplier with this email already exists."
                };
            }

            if (phoneExists)
            {
                return new SupplierAddResp
                {
                    statusCode = 2,
                    message = "A supplier with this phone number already exists."
                };
            }
            var supplier = new Entities.Supplier
            {
                Name = req.Name,
                ContactName = req.ContactName,
                Address = req.Address,
                Phone = req.Phone,
                Email = req.Email,
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return new SupplierAddResp
            {
                id = supplier.Id,
                statusCode = 0,
                message = "Insert successful"
            };
        }

        public async Task<SupplierUpdateResp> UpdateSupplier(SupplierUpdateReq req)
        {
            var existingSupplier = await _context.Suppliers.FindAsync(req.Id);

            if (existingSupplier == null)
            {
                return new SupplierUpdateResp
                {
                    statusCode = 1,
                    message = "Supplier not found."
                };
            }

            existingSupplier.Name = req.Name;
            existingSupplier.ContactName = req.ContactName;
            existingSupplier.Address = req.Address;
            existingSupplier.Phone = req.Phone;
            existingSupplier.Email = req.Email;
            existingSupplier.Updated = DateTime.Now;

            _context.Suppliers.Update(existingSupplier);
            await _context.SaveChangesAsync();

            return new SupplierUpdateResp
            {
                statusCode = 0,
                message = "Update successful"
            };
        }


        public async Task<SupplierDeleteResp> DeleteSupplier(SupplierDeleteReq req)
        {
            var supplier = await _context.Suppliers.FindAsync(req.Id);

            if (supplier == null)
            {
                return new SupplierDeleteResp
                {
                    statusCode = 1,
                    message = "Supplier not found."
                };
            }

            supplier.IsActive = false;
            supplier.Updated = DateTime.Now;

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return new SupplierDeleteResp
            {
                statusCode = 0,
                message = "Supplier deactivated successfully"
            };
        }

        public async Task<SupplierByIdResp> GetSupplierById(SupplierByIdReq req)
        {
            var supplier = await _context.Suppliers.FindAsync(req.Id);

            if (supplier == null)
            {
                return new SupplierByIdResp
                {
                    statusCode = 1,
                    message = "Supplier not found."
                };
            }

            return new SupplierByIdResp
            {
                statusCode = 0,
                message = "Supplier found.",
                data = supplier
            };
        }
    }
}
