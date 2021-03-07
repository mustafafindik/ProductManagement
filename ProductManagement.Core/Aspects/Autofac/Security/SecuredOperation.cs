using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.Constants;
using ProductManagement.Core.Extensions;
using ProductManagement.Core.Utilities.Interceptors;
using ProductManagement.Core.Utilities.IoC;

namespace ProductManagement.Core.Aspects.Autofac.Security
{
    public class SecuredOperation : MethodInterception
    {
       
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

 
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceHelper.ServiceProvider.GetService<IHttpContextAccessor>();

        }


        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new System.UnauthorizedAccessException(Messages.AuthorizationDenied);
        }
    }
}
