using DryIocSample.ComponentManagement.Attributes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryIocSample.Services
{
    [DependencyComponent]
    public class FooDerivedService : IFooService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public void Foo()
        {
            logger.Trace("Foo Derived");
        }
    }
}
