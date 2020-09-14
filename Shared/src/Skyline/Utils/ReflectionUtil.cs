using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Skyline.Utils
{
    public static class ReflectionUtil
    {
        public static string GetDescription(MemberInfo member)
        {
            if (member.IsNull())
                return string.Empty;
            return member.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute ? attribute.Description : member.Name;
        }
    }
}
