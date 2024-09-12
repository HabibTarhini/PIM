using Microsoft.EntityFrameworkCore;
using PIM.Entities.Request;
using PIM.Entities.Respone;
using PIM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Repository;

namespace PIM.Business.PurchaseOrder
{
    public class PurchaseOrderBusiness : IPurchaseOrderBusiness
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<PurchaseOrderCreateResp> CreatePurchaseOrder(PurchaseOrderCreateReq req)
        {
            // Step 1: Check if supplier exists
            var supplier = await _context.Suppliers.FindAsync(req.SupplierId);
            if (supplier == null)
            {
                return new PurchaseOrderCreateResp
                {
                    statusCode = 1,
                    message = "Supplier not found."
                };
            }

            decimal totalAmount = 0;
            var insufficientStockItems = new List<string>();

            foreach (var item in req.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null || product.Quantity < item.Quantity)
                {
                    insufficientStockItems.Add(product?.ProductName ?? "Unknown Product");
                }
                else
                {
                    var unitPrice = product.Price;

                    totalAmount += item.Quantity * unitPrice;
                }
            }

            if (insufficientStockItems.Count > 0)
            {
                return new PurchaseOrderCreateResp
                {
                    statusCode = 2,
                    message = $"Insufficient stock for: {string.Join(", ", insufficientStockItems)}"
                };
            }

            var purchaseOrder = new Entities.PurchaseOrder
            {
                SupplierId = req.SupplierId,
                TotalAmount = totalAmount,
                StatusId = 1, // Default to 'Pending'
                              // OrderDate = DateTime.Now // Set the order date to now
            };
            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            foreach (var item in req.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    var unitPrice = product.Price;

                    var orderItem = new PurchaseOrderItem
                    {
                        PurchaseOrderId = purchaseOrder.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = unitPrice,
                        Price = item.Quantity * unitPrice
                    };
                    _context.PurchaseOrderItems.Add(orderItem);
                }
            }

            await _context.SaveChangesAsync();

            return new PurchaseOrderCreateResp
            {
                statusCode = 0,
                message = "Purchase order created successfully.",
                OrderId = purchaseOrder.Id,
                TotalAmount = totalAmount
            };
        }

        public async Task<PurchaseOrderUpdateStatusResp> UpdatePurchaseOrderStatus(PurchaseOrderUpdateStatusReq req)
        {
            var purchaseOrder = await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderItems)
                .FirstOrDefaultAsync(po => po.Id == req.OrderId);

            if (purchaseOrder == null)
            {
                return new PurchaseOrderUpdateStatusResp
                {
                    statusCode = 1,
                    message = "Purchase order not found."
                };
            }

            var supplier = await _context.Suppliers.FindAsync(purchaseOrder.SupplierId);
            if (supplier == null)
            {
                return new PurchaseOrderUpdateStatusResp
                {
                    statusCode = 2,
                    message = "Associated supplier not found."
                };
            }

            purchaseOrder.StatusId = req.StatusId;
            purchaseOrder.Updated = DateTime.Now;

            _context.PurchaseOrders.Update(purchaseOrder);
            await _context.SaveChangesAsync();

            return new PurchaseOrderUpdateStatusResp
            {
                statusCode = 0,
                message = "Purchase order status updated successfully."
            };
        }

        //private async Task ReplenishStockAsync(Entities.PurchaseOrder purchaseOrder)
        //{
        //    foreach (var item in purchaseOrder.PurchaseOrderItems)
        //    {
        //        var product = await _context.Products.FindAsync(item.ProductId);
        //        if (product != null)
        //        {
        //            // Increase the product quantity by the order quantity
        //            product.Quantity += item.Quantity;

        //            _context.Products.Update(product);
        //        }
        //    }

        //    await _context.SaveChangesAsync();
        //}
    }
}
