using Castle.DynamicProxy;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DryIocSample.ComponentManagement.Attributes;

namespace DryIocSample.ComponentManagement.Interceptors
{
    public class TraceInterceptorAsync : IAsyncInterceptor
    {
        public TraceInterceptorAsync()
        {
        }
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            if (!InterceptEnabled(invocation))
            {
                invocation.Proceed();
                return;
            }

            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            this.Trace("method start", invocation, logger);
            invocation.Proceed();
            this.Trace($"method end with return value {invocation.ReturnValue}", invocation, logger);
        }
        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            if (!InterceptEnabled(invocation))
            {
                await Invoke(invocation);
                return;
            }

            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            this.Trace("async method start", invocation, logger);
            await Invoke(invocation);
            this.Trace("async method end", invocation, logger);
        }


        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {

            if (!InterceptEnabled(invocation))
            {
                return await InvokeAsync<TResult>(invocation);
            }

            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            this.Trace("method start", invocation, logger);
            TResult result = await InvokeAsync<TResult>(invocation);
            this.Trace($"method end with return value {result}", invocation, logger);
            return result;
        }
        private bool InterceptEnabled(IInvocation invocation)
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
        private async Task Invoke(IInvocation invocation)
        {
            invocation.Proceed();
            var task = (Task)invocation.ReturnValue;
            await task;
        }

        private async Task<TResult> InvokeAsync<TResult>(IInvocation invocation)
        {
            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            TResult result = await task;
            return result;
        }
    }
}
