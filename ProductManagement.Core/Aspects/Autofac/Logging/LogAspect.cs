using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductManagement.Core.CrossCuttingConcerns.Logging;
using ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog;
using ProductManagement.Core.Utilities.Interceptors;
using ProductManagement.Core.Utilities.IoC;

namespace ProductManagement.Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException("Messages.WrongLoggerType");
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceHelper.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// Loglama İşlemden önce yapılır. Info
        /// </summary>
        /// <param name="invocation"></param>
        protected override void OnBefore(IInvocation invocation)
        {

            _loggerServiceBase?.Info(GetLogDetail(invocation));
        }

        /// <summary>
        /// Burada Gerekli Mesaj oluşturulur.
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private string GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,


                });
            }
            var logDetail = new LogDetail
            {
                FullName = invocation.Method.DeclaringType?.FullName,
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                User = (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User.Identity.Name == null) ? "?" : _httpContextAccessor.HttpContext.User.Identity.Name

            };
            return JsonConvert.SerializeObject(logDetail);
        }
    }
}
