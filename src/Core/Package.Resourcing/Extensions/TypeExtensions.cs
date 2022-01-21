using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Resourcing.Extensions
{
    public static class TypeExtensions
    {      
        public static Type? GetGenericArgument(this Type checkedType, Type type, int index)
        {
            Type cell = checkedType;            
            while (cell != null)
            {
                if (cell.Name == type.Name &&
                    cell.Namespace == type.Namespace &&
                    cell.Assembly == type.Assembly)
                {
                    var args = cell.GetGenericArguments();
                    if (args.Length == 0 || args.Length <= index)
                        return null;
                    return args[index];                   
                }
                cell = cell.BaseType;
            }
            return null;
        }
    }
}
