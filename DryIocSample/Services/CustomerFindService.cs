using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIocSample.ComponentManagement.Attributes;
using NLog;
namespace DryIocSample.Services
{
    [DependencyComponent]
    public class CustomerFindService : ICustomerFindService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        [Trace]
        public virtual async Task FindAsync()
        {
            await Task.Run(()=>{
                logger.Trace("find async");
            });
        }
        [Trace]
        public virtual void Find()
        {
            logger.Trace("find sync");
        }
    }
}
