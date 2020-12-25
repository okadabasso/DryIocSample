using DryIocSample.ComponentManagement.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace DryIocSample.ComponentManagement
{
    public static class ComponentTypeHelper
    {
        public static bool IsComponentInterface(this Type t)
        {
            return t.IsInterface && t.GetCustomAttribute<DependencyComponentAttribute>() != null;
        }
        public static bool IsDependencyComponent(this Type t)
        {
            return t.IsClass && t.GetCustomAttribute<DependencyComponentAttribute>() != null;
        }
    }
}
