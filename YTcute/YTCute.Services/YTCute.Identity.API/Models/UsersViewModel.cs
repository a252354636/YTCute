using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTCute.Identity.API.Models
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Remark { get; set; }
        public string ReturnUrl { get; set; }
    }
}
