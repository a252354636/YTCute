using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTCute.Identity.API.Data;
using YTCute.Identity.API.Models;

namespace YTCute.Identity.API.Services
{
    public class UsersService : IUsersService
    {
        public EFContext db;
        public UsersService(EFContext _efContext)
        {
            db = _efContext;
        }
        public async Task<Users> GetByStr(string username, string pwd)
        {

            Users m = await db.Users.Where(a => a.UserName == username && a.Password == pwd).SingleOrDefaultAsync();
            if (m != null)
            {
                return m;
            }
            else
            {
                return null;
            }
        }
        public async Task<Users> GetByID(string username)
        {

            Users m = await db.Users.Where(a => a.UserName == username).SingleOrDefaultAsync();
            if (m != null)
            {
                return m;
            }
            else
            {
                return null;
            }
        }
    }
}
