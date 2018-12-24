using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTCute.Identity.API.Models;

namespace YTCute.Identity.API.Services
{
    public interface IUsersService
    {
         Task<Users> GetByStr(string username, string pwd);//根据用户名和密码查找用户
        Task<Users> GetByID(string id);
    }
}
