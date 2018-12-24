
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using YTCute.Web.UI.Models;

namespace YTCute.Web.UI.Services
{
    public class IdentityParser:IIdentityParser<ApplicationUser>
    {
        public ApplicationUser Parse(IPrincipal principal)
        {
            // Pattern matching 'is' expression
            // assigns "claims" if "principal" is a "ClaimsPrincipal"
            if (principal is ClaimsPrincipal claims)
            {
                return new ApplicationUser
                {

                    UserName = claims.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value ?? "",
                    Password = claims.Claims.FirstOrDefault(x => x.Type == "Password")?.Value ?? "",
                    Remark = claims.Claims.FirstOrDefault(x => x.Type == "Remark")?.Value ?? "",
                    Id = int.Parse(claims.Claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? "0")

                };
            }
            throw new ArgumentException(message: "The principal must be a ClaimsPrincipal", paramName: nameof(principal));
        }
    }
}


