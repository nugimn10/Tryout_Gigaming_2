using System;
using Microsoft.EntityFrameworkCore;
using payment_services.Domain.Entities;

namespace payment_services.Presistences
{
    public class payment_context : DbContext
    {
        public payment_context (DbContextOptions<payment_context> options) : base(options) {}
        public DbSet<OrdersTb> orders {get; set;}
        public DbSet<PaymentTb> payments {get; set;}
        public DbSet<Order_detailTb> orders_detail {get; set;}
        public DbSet<ProductTb> products {get; set;}

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<PaymentTb>()
            .HasOne(i => i.Orders)
            .WithMany().HasForeignKey(i => i.Order_id);
            
            model.Entity<Order_detailTb>()
            .HasOne(i => i.product)
            .WithMany().HasForeignKey(i => i.product_id);
            model.Entity<Order_detailTb>()
            .HasOne(i => i.orders)
            .WithMany().HasForeignKey(i => i.order_id);
            
        }
    }
}