using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Skyline.Mongo
{
    internal static class Extensions
    {
        internal static SortDefinition<T> GetSortDefinition<T>(this Func<Sort<T>, Sort<T>> sort)
        {
            List<SortItem> sortList = sort(new Sort<T>());

            return Builders<T>.Sort.Combine(sortList.Select(sortItem => sortItem.SortType == ESort.Asc
                ? Builders<T>.Sort.Ascending(sortItem.FieldName)
                : Builders<T>.Sort.Descending(sortItem.FieldName))
                .ToList());
        }

        internal static UpdateDefinition<T> GetUpdateDefinition<T>(this T entity)
        {
            var properties = typeof(T).GetEntityProperties();

            var updateDefinitionList = GetUpdateDefinitionList<T>(properties, entity);

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);

            return updateDefinitionBuilder;
        }

        internal static List<UpdateDefinition<T>> GetUpdateDefinitionList<T>(PropertyInfo[] propertyInfos, object entity)
        {
            var updateDefinitionList = new List<UpdateDefinition<T>>();

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name == "Id")
                    continue;

                if (propertyInfo.PropertyType.IsArray || Types.IList.IsAssignableFrom(propertyInfo.PropertyType))
                {
                    var value = propertyInfo.GetValue(entity) as IList;

                    var filedName = propertyInfo.Name;

                    updateDefinitionList.Add(Builders<T>.Update.Set(filedName, value));
                }
                else
                {
                    var value = propertyInfo.GetValue(entity);

                    if (propertyInfo.PropertyType == Types.Decimal)
                        value = value.ToString();
                    else if (propertyInfo.PropertyType.IsEnum)
                        value = (int)value;

                    var filedName = propertyInfo.Name;

                    updateDefinitionList.Add(Builders<T>.Update.Set(filedName, value));
                }
            }

            return updateDefinitionList;
        }

        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertiesDic = new ConcurrentDictionary<Type, PropertyInfo[]>();
        public static PropertyInfo[] GetEntityProperties(this Type type)
        {
            return PropertiesDic.GetOrAdd(type, item => type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
        }
    }
}