using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using DryIocSample.ComponentManagement;
using DryIocSample.ComponentManagement.Interceptors;
using DryIocSample.Models;
using DryIocSample.Services;

namespace DryIocSample
{
    public class Startup
    {
        public static IContainer BuildContainer()
        {

            var container = new DryIoc.Container();


            container.Register<SampleDbContext>(reuse: Reuse.Scoped);
            container.Register<TraceInterceptor>(reuse: Reuse.Transient);
            container.Register<TraceInterceptorAsync>(reuse: Reuse.Transient);
            container.RegisterMany(
                typeof(ICustomerFindService).Assembly.GetTypes().Where(x => x.IsDependencyComponent()),
                serviceTypeCondition: t => t.IsComponentInterface(),
                reuse: Reuse.Scoped,
                ifAlreadyRegistered: IfAlreadyRegistered.Replace

            );


            container.Intercept<TraceInterceptor>(t => t.IsComponentInterface());
            return container;
        }
    }
}
