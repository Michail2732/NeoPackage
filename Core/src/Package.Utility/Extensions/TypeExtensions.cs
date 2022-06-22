using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Extensions
{
    public static class TypeExtensions
    {      
        public static Type? GetGenericArgument(this Type sourceType, Type genericType, int index)
        {            
            if (genericType.IsInterface)
            {
                var sourceTypeInterfaces = sourceType.GetInterfaces();
                foreach (var sourceTypeInterface in sourceTypeInterfaces)
                {
                    if (sourceTypeInterface.IsGenericType && sourceTypeInterface.GetGenericTypeDefinition() == genericType)
                    {
                        var sourceTypeGenericArgs = sourceTypeInterface.GetGenericArguments();
                        if (sourceTypeGenericArgs.Length == 0 || sourceTypeGenericArgs.Length - 1 < index)
                            return null;
                        return sourceTypeGenericArgs[index];
                    }
                }
            }
            Type? cell = sourceType;
            while (cell != null)
            {               
                if (cell.IsGenericType && cell.GetGenericTypeDefinition() == genericType)
                {
                    var sourceTypeGenericArgs = cell.GetGenericArguments();
                    if (sourceTypeGenericArgs.Length == 0 || sourceTypeGenericArgs.Length - 1 < index)
                        return null;
                    return sourceTypeGenericArgs[index];                    
                }
                cell = cell.BaseType;
            }
            return null;
        }

        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
