using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Skyline.Utils
{
    public static partial class EnumUtil
    {
        public static IDictionary<int, string> GetDictionary<TEnum>() where TEnum : struct
        {
            var enumType = typeof(TEnum).GetTypeInfo();
            ValidateEnum(enumType);
            var dic = new Dictionary<int, string>();
            foreach (var field in enumType.GetFields())
                AddItem<TEnum>(dic, field);
            return dic;
        }

        public static int GetValue<TEnum>(object member)
        {
            return GetValue(typeof(TEnum), member);
        }

        public static int GetValue(Type type, object member)
        {
            if (member.IsNull())
                throw new ArgumentNullException(nameof(member));
            return (int)System.Enum.Parse(type, member.ToString(), true);
        }

        private static void ValidateEnum(Type enumType)
        {
            if (enumType.IsEnum == false)
                throw new InvalidOperationException($"类型 {enumType} 不是枚举");
        }

        private static void AddItem<TEnum>(IDictionary<int, string> result, FieldInfo field) where TEnum : struct
        {
            if (!field.FieldType.GetTypeInfo().IsEnum)
                return;
            var value = GetValue<TEnum>(field.Name);
            var description = ReflectionUtil.GetDescription(field);
            result.Add(value, description);
        }
    }
}
