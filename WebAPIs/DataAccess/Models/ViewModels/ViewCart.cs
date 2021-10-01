using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.ViewModels
{
    public class Cart
    {
        public int CusId { get; set; }
        public List<CartItem> ListViewCart { get; set; } = new List<CartItem>();
        public double TotalPrice { get; set; }
    }

    public class CartItem
    {
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
        public double Sotien { get; set; }
    }
    public class ViewCart
    {
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
    }
}
