using DataAccess.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.BUS
{
    public class DishBUS
    {
        readonly DataContext _context = null;
        public DishBUS()
        {
            _context = new DataContext();
        }
        public IEnumerable<Dish> GetAllDishes(string search, int page, int pageSize)
        {
            IEnumerable<Dish> model = _context.Dishes;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.ToUpper().Contains(search.ToUpper()) || x.Category.ToString().ToUpper().Contains(search.ToUpper()));
            }
            return model.OrderBy(x => x.DishId).ToPagedList(page, pageSize);
        }
        public IEnumerable<Dish> GetAllDishes(string search)
        {
            IEnumerable<Dish> model = _context.Dishes;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.ToUpper().Contains(search.ToUpper()) || x.Category.ToString().ToUpper().Contains(search.ToUpper()));
            }
            return model.ToList();
        }
        public Dish GetDishById(int id)
        {
            return _context.Dishes.Find(id);
        }
        public int AddDish(Dish dish)
        {
            int value = 0;
            try
            {
                _context.Dishes.Add(dish);
                _context.SaveChanges();
                value = dish.DishId;
            }
            catch (Exception)
            {
                value = 0;
            }
            return value;
        }
        public bool UpdateDish(Dish dish)
        {
            try
            {
                var _dish = _context.Dishes.Find(dish.DishId);
                _dish.Name = dish.Name;
                _dish.Price = dish.Price;
                _dish.Category = dish.Category;
                _dish.Image = dish.Image;
                _dish.Description = dish.Description;
                _dish.Status = dish.Status;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteDish(int id)
        {
            try
            {
                var dish = _context.Dishes.Find(id);
                _context.Dishes.Remove(dish);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
