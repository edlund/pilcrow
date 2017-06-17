
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pilcrow.Core.Helpers
{
    public abstract class TypeHelper
    {
        public static IEnumerable<Type> GetSubClassTypes(Type parent)
        {
            var types = new List<Type>();
            var assemblies = AssemblyHelper.GetRuntimeAssemblies();
            foreach (var assembly in assemblies) {
                types.AddRange(
                    from type in assembly.GetTypes()
                    where type.GetTypeInfo().IsSubclassOf(parent)
                    select type
                );
            }
            return types.OrderBy(type => type.Name);
        }
        
        public static IEnumerable<Type> GetDirectSubClassTypes(Type parent)
        {
            var allTypes = GetSubClassTypes(parent);
            return from currentType in allTypes
                where allTypes.Aggregate(true, (
                    directType,
                    otherType
                ) => directType && !currentType.GetTypeInfo().IsSubclassOf(otherType))
                select currentType
            ;
        }
    }
}
