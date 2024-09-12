using PIM.Entities.Request;
using PIM.Entities.Respone;
using PIM.Entities;
using PIM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace PIM.Business.PurchaseReceiptBusiness
{
    public class PurchaseReceiptBusiness : IPurchaseReceiptBusiness
    {
        private readonly ApplicationDbContext _context;

        public PurchaseReceiptBusiness(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PurchaseReceiptCreateResp> CreatePurchaseReceipt(PurchaseReceiptCreateReq req)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                var purchaseOrder = await _context.PurchaseOrders
                                              .Include(p => p.PurchaseOrderItems)
                                              .FirstOrDefaultAsync(p => p.Id == req.PurchaseOrderId);
                //Check if purchase order is completed and order is available
                if (purchaseOrder == null || purchaseOrder.StatusId != 2)
                {
                    return new PurchaseReceiptCreateResp { statusCode = 1, message = "Invalid or incomplete purchase order." };
                }

                var receipt = new PurchaseReceipt
                {
                    PurchaseOrderId = req.PurchaseOrderId,
                    ReceiptDate = DateTime.UtcNow
                };

                _context.PurchaseReceipts.Add(receipt);
                await _context.SaveChangesAsync();

                decimal totalPrice = 0m;
                int totalItems = 0;

                //Check each item the product and update inventory
                foreach (var item in req.Items)
                {
                    var orderItem = purchaseOrder.PurchaseOrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (orderItem == null || item.ReceivedQuantity > orderItem.Quantity)
                    {
                        return new PurchaseReceiptCreateResp { statusCode = 2, message = "Invalid received quantity." };
                    }

                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        if (product.Quantity >= item.ReceivedQuantity)
                        {
                            product.Quantity -= item.ReceivedQuantity;
                        }
                        else
                        {
                            return new PurchaseReceiptCreateResp
                            {
                                statusCode = 3,
                                message = $"Insufficient stock to fulfill receipt for product: {product.ProductName}."
                            };
                        }
                    }

                    decimal unitPrice = orderItem.UnitPrice;

                    if (unitPrice <= 0)
                    {
                        return new PurchaseReceiptCreateResp { statusCode = 4, message = "Invalid unit price for product." };
                    }

                    var receiptItem = new PurchaseReceiptItem
                    {
                        PurchaseReceiptId = receipt.Id,
                        ProductId = item.ProductId,
                        ReceivedQuantity = item.ReceivedQuantity,
                        UnitPrice = unitPrice
                    };

                    _context.PurchaseReceiptItems.Add(receiptItem);

                    totalPrice += unitPrice * item.ReceivedQuantity;
                    totalItems += item.ReceivedQuantity;
                }

                receipt.TotalPrice = totalPrice;
                receipt.TotalItems = totalItems;

                _context.PurchaseReceipts.Update(receipt);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new PurchaseReceiptCreateResp { statusCode = 0, message = "Purchase receipt created successfully." };
            }
        }
    public async Task<PurchaseReceiptViewResp> ViewPurchaseReceipt(PurchaseReceiptViewReq req)
        {
            var receipt = await _context.PurchaseReceipts.Include(r => r.PurchaseReceiptItems)
                                                         .FirstOrDefaultAsync(r => r.Id == req.ReceiptId);
            if (receipt == null)
            {
                return new PurchaseReceiptViewResp { StatusCode = 1, Message = "Receipt not found." };
            }

            var items = receipt.PurchaseReceiptItems.Select(i => new PurchaseReceiptItemViewResp
            {
                ProductId = i.ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.Id == i.ProductId)?.ProductName,
                ReceivedQuantity = i.ReceivedQuantity
            }).ToList();

            return new PurchaseReceiptViewResp
            {
                StatusCode = 0,
                Message = "Receipt retrieved successfully.",
                ReceiptId = receipt.Id,
                PurchaseOrderId = receipt.PurchaseOrderId,
                Items = items
            };
        }
    }
}
