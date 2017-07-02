using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace Pilcrow.Core.Helpers
{
    public static class AssemblyHelper
    {
        public static IEnumerable<Assembly> GetRuntimeAssemblies(Func<AssemblyName, bool> filter)
        {
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();
            var runtimeAssemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId);
            return from runtimeAssemblyName in runtimeAssemblyNames
                where filter(runtimeAssemblyName)
                select Assembly.Load(runtimeAssemblyName);
        }
        
        public static IEnumerable<Assembly> GetRuntimeAssemblies()
        {
            return GetRuntimeAssemblies(assemblyName => true);
        }
    }
}
