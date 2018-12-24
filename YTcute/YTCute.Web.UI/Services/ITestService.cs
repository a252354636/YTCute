using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTCute.Web.UI.Models;

namespace YTCute.Web.UI.Services
{
    public interface ITestService
    {
          Task<string> GetTest(ApplicationUser user, string id);
    }
}
