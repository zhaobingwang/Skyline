using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Blocks.Domain
{
    public abstract class Entity : IEntity
    {
        public abstract object[] GetKeys();

        public override string ToString()
        {
            return $"[ENTITY]: {GetType().Name} Keys = {GetKeys().JoinAsString(", ")}";
        }
    }

    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }
        protected Entity()
        {

        }
        protected Entity(TKey id)
        {
            Id = id;
        }

        public bool EntityEquals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
            {
                return false;
            }

            // 相同的实例视为相等
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // 瞬态对象视为不等
            var other = (Entity<TKey>)obj;
            if (EntityHelper.HasDefaultId(this) && EntityHelper.HasDefaultId(other))
            {
                return false;
            }

            // 必须具有 IS-A关系或者必须是相同类型
            var typeOfThis = GetType().GetTypeInfo();
            var typeOfOther = other.GetType().GetTypeInfo();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }
    }
}
