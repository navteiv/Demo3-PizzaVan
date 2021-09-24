using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.ViewModels
{
    public class ViewToken
    {
        //[Required]
        public string Token { get; set; }

        //[Required]
        public int Id { get; set; }

        //public string ReturnUrl { get; set; }
        public string FullName { get; set; }
    }
}
