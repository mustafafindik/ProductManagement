using System;
using Castle.DynamicProxy;

namespace ProductManagement.Core.Utilities.Interceptors
{
    /// <summary>
    ///  Priority Burada Önem Sırası belirmek için Kullanılır.. 
    ///  AttributeTargets.Class = Sınıfın En tepesinde.
    /// AttributeTargets.Method =  Metodlarda kullanalıabilir.
    /// AllowMultiple : birden flazla yerde kullanılabilir.
    /// Inherited = miras alan sınıflar kullanablir.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        /// <summary>
        ///  İlerde ezmek gerektiği için virtual veriyoruz.
        /// </summary>
        /// <param name="invocation"></param>
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
