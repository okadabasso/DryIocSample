using DryIocSample.ComponentManagement.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryIocSample.Services
{
    [DependencyComponent]
    public interface IFooService
    {
        void Foo();
    }
}
