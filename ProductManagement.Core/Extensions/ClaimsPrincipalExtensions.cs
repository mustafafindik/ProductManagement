using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        private static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }

        public static int ClaimId(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.FindAll(ClaimTypes.NameIdentifier)?.Select(x => x.Value).FirstOrDefault();
            return Int32.Parse(result!);
        }

    }
}
