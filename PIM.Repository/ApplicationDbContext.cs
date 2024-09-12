using Microsoft.EntityFrameworkCore;
using PIM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseReceipt> PurchaseReceipts { get; set; }
        public DbSet<PurchaseReceiptItem> PurchaseReceiptItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.ContactName).HasMaxLength(255);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("PurchaseOrder");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SupplierId).IsRequired();
                entity.Property(e => e.TotalAmount).IsRequired().HasColumnType("decimal(18, 2)");
                entity.Property(e => e.StatusId).IsRequired();
                entity.Property(e => e.Created).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Updated).IsRequired(false);
                //entity.Property(e => e.OrderDate).IsRequired();

                entity.HasOne<Supplier>()
                      .WithMany()
                      .HasForeignKey(e => e.SupplierId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.PurchaseOrderItems)
                      .WithOne(poi => poi.PurchaseOrder)
                      .HasForeignKey(poi => poi.PurchaseOrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.ToTable("PurchaseOrderItem");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PurchaseOrderId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.UnitPrice).IsRequired().HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.PurchaseOrder)
                      .WithMany(p => p.PurchaseOrderItems)
                      .HasForeignKey(e => e.PurchaseOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                // entity.HasOne(e => e.Product)
                //       .WithMany()
                //       .HasForeignKey(e => e.ProductId);
            });
            modelBuilder.Entity<PurchaseReceipt>(entity =>
            {
                entity.ToTable("purchasereceipt");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PurchaseOrderId).HasColumnName("purchaseorder_id").IsRequired();
                entity.Property(e => e.ReceiptDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.TotalItems).IsRequired();
                entity.Property(e => e.TotalPrice).IsRequired().HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Created).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Updated);

                /*entity.HasOne<PurchaseOrder>()
                      .WithMany()
                      .HasForeignKey(e => e.PurchaseOrder_Id)
                      .OnDelete(DeleteBehavior.Cascade);*/
            });

            modelBuilder.Entity<PurchaseReceiptItem>(entity =>
            {
                entity.ToTable("purchasereceiptitem");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PurchaseReceiptId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.ReceivedQuantity).IsRequired();
                entity.Property(e => e.UnitPrice).IsRequired().HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Created).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Updated);

                /*entity.HasOne(e => e.PurchaseReceipt)
                      .WithMany(pr => pr.PurchaseReceiptItems)
                      .HasForeignKey(e => e.PurchaseReceiptId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);*/
            });            
        }
    }
}


