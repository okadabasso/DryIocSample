using DryIoc;
using DryIocSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryIocSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Startup.BuildContainer();
            using(var scope = container.OpenScope())
            {
                var service = scope.Resolve<ICustomerFindService>();
                service.Find();
                var result = Task.Run(() => {
                    service.FindAsync();
                });
                result.Wait();

                var foo = scope.Resolve<IFooService>();
                foo.Foo();


            }

            Console.ReadLine();
        }
    }
}
