using Microsoft.EntityFrameworkCore;
using PIM.Entities.Request;
using PIM.Entities.Respone;
using PIM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Business.ProductBusiness
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly ApplicationDbContext _context;

        public ProductBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductAddResp> AddProduct(ProductAddReq req)
        {
            var product = new Entities.Product
            {
                ProductName = req.ProductName,
                Price = req.Price,
                Quantity = req.Quantity
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductAddResp
            {
                id = product.Id,
                statusCode = 0, // Success
                message = "Product added successfully"
            };
        }

        public async Task<ProductUpdateResp> UpdateProduct(ProductUpdateReq req)
        {
            var existingProduct = await _context.Products.FindAsync(req.id);

            if (existingProduct == null)
            {
                return new ProductUpdateResp
                {
                    statusCode = 1, // Not found
                    message = "Product not found."
                };
            }

            existingProduct.ProductName = req.ProductName;
            existingProduct.Price = req.Price;
            existingProduct.Quantity = req.Quantity;
            //existingProduct.Updated = DateTime.Now;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return new ProductUpdateResp
            {
                statusCode = 0, // Success
                message = "Product updated successfully"
            };
        }

        public async Task<ProductDeleteResp> DeleteProduct(ProductDeleteReq req)
        {
            var product = await _context.Products.FindAsync(req.id);

            if (product == null)
            {
                return new ProductDeleteResp
                {
                    statusCode = 1, // Not found
                    message = "Product not found."
                };
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ProductDeleteResp
            {
                statusCode = 0, // Success
                message = "Product deleted successfully"
            };
        }

        public async Task<ProductListResp> GetProductList(ProductListReq req)
        {
            var response = new ProductListResp();

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(req.Search))
            {
                var searchLower = req.Search.ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(searchLower));
            }

            var totalCount = await query.CountAsync();

            Console.WriteLine($"Total Count: {totalCount}");

            var products = await query
                .Skip((req.PageNumber - 1) * req.PageSize)
                .Take(req.PageSize)
                .Select(p => new ProductObj
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .ToListAsync();

            Console.WriteLine($"Products Retrieved: {products.Count}");

            response.list = products;
            response.totalCount = totalCount;
            response.statusCode = 0;
            response.message = "Product list retrieved successfully.";

            return response;
        }


        public async Task<ProductByIdResp> GetProductById(ProductByIdReq req)
        {
            var product = await _context.Products
                .Where(p => p.Id == req.id)
                .Select(p => new ProductObj
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return new ProductByIdResp
                {
                    statusCode = 1, // Not Found
                    message = "Product not found."
                };
            }

            return new ProductByIdResp
            {
                statusCode = 0, // Success
                message = "Product retrieved successfully.",
                data = product
            };
        }

    }
}
