using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryIocSample.Models
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext()
            : base("default")
        {

        }
    }
}
