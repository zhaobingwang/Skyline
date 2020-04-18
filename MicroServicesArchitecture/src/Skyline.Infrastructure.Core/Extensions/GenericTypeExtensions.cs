using System;
using System.Linq;

namespace Skyline.Infrastructure.Core.Extensions
{
    public static class GenericTypeExtensions
    {
        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;
            if (type.IsGenericType)
            {
                var genericType = string.Join(",", type.GetGenericArguments().Select(x => x.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericType}>";
            }
            else
            {
                typeName = type.Name;
            }
            return typeName;
        }

        public static string GetGenericTypeName(this object obj)
        {
            return obj.GetType().GetGenericTypeName();
        }
    }
}
