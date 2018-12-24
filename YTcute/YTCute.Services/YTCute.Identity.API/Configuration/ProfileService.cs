using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YTCute.Identity.API.Models;
using YTCute.Identity.API.Services;

namespace YTCute.Identity.API.Configuration
{
    public class ProfileService : IProfileService
    {
        private readonly IUsersService _usersService;

        public ProfileService(IUsersService usersService )
        {

            _usersService = usersService;
        }

        public async  Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

            var user = await _usersService.GetByID(subjectId);
            if (user == null)
                throw new ArgumentException("Invalid subject identifier");

            var claims = GetClaimsFromUser(user);
            context.IssuedClaims = claims.ToList();
        }

         public async Task IsActiveAsync(IsActiveContext context)
        {
            
           
            //var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            //var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            //var user = await _usersService.GetByID(subjectId);

            //context.IsActive = false;

            //if (user != null)
            //{

            //        var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
            //        if (security_stamp != null)
            //        {
            //            //var db_security_stamp = await _userManager.GetSecurityStampAsync(user);
            //            //if (db_security_stamp != security_stamp)
            //            //    return;
            //        }
            //    context.IsActive =
            //        !user.LockoutEnabled ||
            //        !user.LockoutEnd.HasValue ||
            //        user.LockoutEnd <= DateTime.Now;
            //}
        }

        private IEnumerable<Claim> GetClaimsFromUser(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.UserName.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            if (!string.IsNullOrWhiteSpace(user.UserName))
                claims.Add(new Claim("UserName", user.UserName));

            if (!string.IsNullOrWhiteSpace(user.Remark))
                claims.Add(new Claim("Remark", user.Remark));
            return claims;
        }
    }
}
