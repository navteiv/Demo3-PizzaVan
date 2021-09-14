using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                 .HasOne<Customer>(cus => cus.Customer)
                 .WithMany(ord => ord.Orders)
                 .HasForeignKey(cus => cus.CusId);
            //modelBuilder.Entity<OrderDetail>()
            //    .HasOne<Order>(ord => ord.Order)
            //    .WithMany(otd => otd.OrderDetails)
            //    .HasForeignKey(ord => ord.OrderId);
            modelBuilder.Entity<OrderDetail>()
                .HasOne<Dish>(dis => dis.Dish)
                .WithMany(otd => otd.OrderDetails)
                .HasForeignKey(dis => dis.DishId);
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
