using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using DryIocSample.ComponentManagement.Attributes;

namespace DryIocSample.ComponentManagement.Interceptors
{
    public class TraceInterceptor : IInterceptor
    {
        IAsyncInterceptor _asyncInterceptor;
        public TraceInterceptor(TraceInterceptorAsync asyncInterceptor)
        {
            _asyncInterceptor = asyncInterceptor;
        }
        public void Intercept(IInvocation invocation)
        {
            if (!InterceptionEnabled(invocation))
            {
                invocation.Proceed();
                return;
            }

            _asyncInterceptor.ToInterceptor().Intercept(invocation);
        }
        private bool InterceptionEnabled(IInvocation invocation)
        {
            if (invocation.TargetType.GetCustomAttribute<DependencyComponentAttribute>() == null)
            {
                return false;
            }
            if (invocation.MethodInvocationTarget.GetCustomAttribute<TraceAttribute>() == null)
            {
                return false;
            }
            return true;
        }
    }
}
