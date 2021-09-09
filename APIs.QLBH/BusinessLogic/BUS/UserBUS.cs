using DataAccess.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BUS
{
    public class UserBUS
    {
        readonly DataContext _context = null;
        public UserBUS()
        {
            _context = new DataContext();
        }
        public IEnumerable<User> GetAllUser(string search, int page, int pageSize)
        {
            IEnumerable<User> model = _context.Users;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.UserName.ToUpper().Contains(search.ToUpper()) || x.FullName.ToUpper().Contains(search.ToUpper()));
            }
            return model.OrderBy(x => x.UserId).ToPagedList(page, pageSize);
        }
        public List<User> GetAllUser()
        {
            List<User> list = _context.Users.ToList();
            return list;
        }
        public User GetUser(string name)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == name);
        }
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }
        public int AddUser(User user)
        {
            int value = 0;
            try
            {
                var duplicatedUser = _context.Users.Count(x => x.UserName == user.UserName);
                if (duplicatedUser > 0)
                {
                    return value;
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                value = user.UserId;
            }
            catch (Exception)
            {
                value = 0;
            }
            return value;
        }
        public bool Login(string userName, string passWord)
        {
            var result = _context.Users.Count(x => x.UserName == userName && x.Password == passWord && x.Locked == false);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateUser(User user)
        {
            try
            {
                var _user = _context.Users.Find(user.UserId);
                _user.UserName = user.UserName;
                _user.FullName = user.FullName;
                _user.Title = user.Title;
                _user.DOB = user.DOB;
                _user.Email = user.Email;
                _user.Admin = user.Admin;
                _user.Locked = user.Locked;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    _user.Password = user.Password;
                    _user.ConfirmPassword = user.Password;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
        public bool Delelte(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                _context.Users.Remove(user);
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
