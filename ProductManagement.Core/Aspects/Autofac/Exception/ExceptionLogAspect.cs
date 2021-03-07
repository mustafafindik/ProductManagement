using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductManagement.Core.CrossCuttingConcerns.Logging;
using ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog;
using ProductManagement.Core.Utilities.Interceptors;
using ProductManagement.Core.Utilities.IoC;

namespace ProductManagement.Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException("Messages.WrongLoggerType");
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceHelper.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        /// <summary>
        /// işlem hata aldığında Buraya girer. Exception da e ile bize gelir.
        /// Burada LogDetailWithException Kullanılarak Normal İnformation Kısmına extra olarak ExceptionMessage eklenir.
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="e">Exception</param>
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var logDetailWithException = GetLogDetail(invocation);

            logDetailWithException.ExceptionMessage = e is AggregateException exception ? string.Join(Environment.NewLine, exception.InnerExceptions.Select(x => x.Message)) : e.Message;
            _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetailWithException = new LogDetailWithException
            {
                FullName = invocation.Method.DeclaringType?.FullName,
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                User = (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User.Identity.Name == null) ? "?" : _httpContextAccessor.HttpContext.User.Identity.Name
            };
            return logDetailWithException;
        }
    }
}