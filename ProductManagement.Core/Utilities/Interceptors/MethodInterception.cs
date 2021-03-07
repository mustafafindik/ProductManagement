using System;
using Castle.DynamicProxy;

namespace ProductManagement.Core.Utilities.Interceptors
{
    /// <summary>
    /// Buradaki  OnBefore , OnAfter, OnException... kullandığı yerde ezilerek içine işlemler yazılacak..
    /// </summary>
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }


        /// <summary>
        /// MethodInterceptionBaseAttribute Attributindeki Intercept Metodunu Ezdik.
        /// </summary>
        /// <param name="invocation"></param>
        public override void Intercept(IInvocation invocation)
        {

            var isSuccess = true; // İşlem başarılı set edildi.
            OnBefore(invocation); //Eğer İşlem Oncesinde bişirsey varsa 
            try
            {
                invocation.Proceed(); // O işlemi Çalıştır.
            }
            catch (Exception e)
            {
                isSuccess = false; // İşlem hata aldı.
                OnException(invocation, e); // Bu ExceptionAspect e mesajın gitmesi için e gönderilir.
                throw;
            }
            finally
            {
                if (isSuccess) //Eger İşlem hata almamış şe
                {
                    OnSuccess(invocation); // Başarılı oldu fonk. çalıştır
                }
            }
            OnAfter(invocation); // Ne olursa olsun Sonrasında Metodunu çaşlıştır.
        }
    }
}