using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BUS
{
    public interface IDishBUS
    {
        Task<List<Dish>> GetMonAnAllAsync();
        List<Dish> GetAllDishes();

        Dish GetDish(int id);

        int AddDish(Dish dish);

        int EditDish(int id, Dish dish);

        List<Dish> GetFoodBySearchString(string name);
    }
    public class DishBUS : IDishBUS
    {
        protected DataContext _context;
        public DishBUS(DataContext context)
        {
            _context = context;
        }
        public List<Dish> GetFoodBySearchString(string name)
        {
            return _context.Dishes.Where(x => x.Name.Contains(name.Trim())).ToList();
        }
        public List<Dish> GetAllDishes()
        {
            List<Dish> list = new List<Dish>();
            list = _context.Dishes.ToList();
            return list;
        }
        public Dish GetDish(int id)
        {
            Dish dish = null;
            dish = _context.Dishes.Find(id);
            return dish;
        }
        public int AddDish(Dish dish)
        {
            int value = 0;
            try
            {
                _context.Add(dish);
                _context.SaveChanges();
                value = dish.DishId;
            }
            catch (Exception) { value = 0; }
            return value;
        }
        public int EditDish(int id, Dish dish)
        {
            int value = 0;
            try
            {
                Dish _dish = null;
                _dish = _context.Dishes.Find(id);

                _dish.Name = dish.Name;
                _dish.Price = dish.Price;
                _dish.Category = dish.Category;
                _dish.Image = dish.Image;
                _dish.Description = dish.Description;
                _dish.Status = dish.Status;

                _context.Update(_dish);
                _context.SaveChanges();
                value = dish.DishId;
            }
            catch (Exception) { value = 0; }
            return value;
        }

        public async Task<List<Dish>> GetMonAnAllAsync()
        {
            List<Dish> list = await _context.Dishes.ToListAsync();
            return list;
        }
    }
}
