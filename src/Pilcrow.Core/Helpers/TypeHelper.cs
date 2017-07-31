using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pilcrow.Core.Helpers
{
    public static class TypeHelper
    {
        public static IEnumerable<Type> GetAllTypes()
        {
            var types = new List<Type>();
            var assemblies = AssemblyHelper.GetRuntimeAssemblies();
            foreach (var assembly in assemblies)
                types.AddRange(assembly.GetTypes());
            return types.OrderBy(type => type.Name);
        }
        
        public static IEnumerable<Type> GetSubTypes(Type parent)
        {
            return from type in GetAllTypes()
                where type.GetTypeInfo().IsSubclassOf(parent)
                select type
            ;
        }
        
        public static IEnumerable<Type> GetDirectSubTypes(Type parent)
        {
            var allTypes = GetSubTypes(parent);
            return from currentType in allTypes
                where allTypes.Aggregate(true, (
                    directType,
                    otherType
                ) => directType && !currentType.GetTypeInfo().IsSubclassOf(otherType))
                select currentType
            ;
        }
        
        public static IEnumerable<Type> GetLeafSubTypes(Type parent)
        {
            var allTypes = GetSubTypes(parent);
            return from currentType in allTypes
                where allTypes.Aggregate(true, (
                    leafType,
                    otherType
                ) => leafType && !otherType.GetTypeInfo().IsSubclassOf(currentType))
                select currentType
            ;
        }
        
        public static IEnumerable<Type> GetImplementingTypes(Type itype)
        {
            if (!itype.GetTypeInfo().IsInterface)
                throw new InvalidOperationException($"\"{itype.FullName}\" is not an interface");
            return from type in GetAllTypes()
                let typeInfo = type.GetTypeInfo()
                where typeInfo.GetInterfaces().Contains(itype)
                where !typeInfo.IsInterface
                select type
            ;
        }
        
        public static IEnumerable<Type> GetConcreteTypes(IEnumerable<Type> types)
        {
            return from type in types
                where !type.GetTypeInfo().IsAbstract
                select type;
        }
    }
}
