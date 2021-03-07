using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using ProductManagement.Core.Aspects.Autofac.Exception;
using ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace ProductManagement.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
     
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes =
                type.GetMethod(method.Name)!.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            // Burada Çagrıldığı gibi (tüm Metodlarda, Bussiness metodları üstünde belirli Metodlardada bağrılabilir.
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
            classAttributes.Add(new ExceptionLogAspect(typeof(MsSqlLogger)));

            //Çalışma sırası Priority e göre
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }

    }
}
