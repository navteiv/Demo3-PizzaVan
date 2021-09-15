using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.ViewModels
{
    public class ViewRegis
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public DateTime? DOB { get; set; }
        public bool Admin { get; set; }
        public bool Locked { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
