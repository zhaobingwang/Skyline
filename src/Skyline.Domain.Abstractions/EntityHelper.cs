using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Domain
{
    /// <summary>
    /// 实体的一些辅助方法
    /// </summary>
    public class EntityHelper
    {
        public static bool HasDefaultId<TKey>(IEntity<TKey> entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default))
            {
                return true;
            }

            // 针对EF Core的变通方法，因为它在连接到dbcontext时将int/long设置为最小值
            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(entity.Id) <= 0;
            }
            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(entity.Id) <= 0;
            }
            return false;
        }
    }
}
