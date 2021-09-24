using BusinessLogic.Helpers;
using DataAccess.Models;
using DataAccess.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BUS
{
    public interface IUserBUS
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        int AddUser(User user);
        int EditUser(int id, User user);
        User Login(ViewLogin viewLogin);
    }
    public class UserBUS : IUserBUS
    {
        protected DataContext _context;
        protected IEncryptHelper _encryptHelper;
        public UserBUS(DataContext context, IEncryptHelper encryptHelper)
        {
            _context = context;
            _encryptHelper = encryptHelper;
        }
        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();
            list = _context.Users.ToList();
            return list;
        }
        public User GetUser(int id)
        {
            User user = null;
            user = _context.Users.Find(id);
            return user;
        }
        public int AddUser(User user)
        {
            int value = 0;
            try
            {
                user.Password = _encryptHelper.MD5Encrypt(user.Password);
                _context.Add(user);
                _context.SaveChanges();
                value = user.UserId;
            }
            catch (Exception) { value = 0; }
            return value;
        }
        
        public int EditUser(int id, User user)
        {
            int value = 0;
            try
            {
                User _user = null;
                _user = _context.Users.Find(id);

                _user.UserName = user.UserName;
                _user.FullName = user.FullName;
                _user.Title = user.Title;
                _user.DOB = user.DOB;
                _user.Email = user.Email;
                _user.Admin = user.Admin;
                _user.Locked = user.Locked;
                if (user.Password != null)
                {
                    user.Password = _encryptHelper.MD5Encrypt(user.Password);
                    _user.Password = user.Password;
                    _user.ConfirmPassword = user.Password;
                }
                _context.Update(_user);
                _context.SaveChanges();

                value = user.UserId;
            }
            catch (Exception) { value = 0; }
            return value;
        }
        public User Login(ViewLogin viewLogin)
        {
            var u = _context.Users.Where(
                p => p.UserName.Equals(viewLogin.UserName) &&
                     p.Password.Equals(_encryptHelper.MD5Encrypt(viewLogin.Password)))
                .FirstOrDefault();
            return u;
        }
    }
}
