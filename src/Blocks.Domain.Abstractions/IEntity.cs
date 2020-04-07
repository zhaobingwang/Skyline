using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Domain
{
    /// <summary>
    /// 定义一个实体（主键可能不是"Id"，或者它可能是一个复合主键）
    /// 使用<see cref="IEntity{TKey}"/>以便更好地集成到框架中的存储库和其他结构
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 返回此实体的有序键数组
        /// </summary>
        /// <returns></returns>
        object[] GetKeys();
    }

    /// <summary>
    /// 用带有"Id"属性的单一主键定义实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> : IEntity
    {
        /// <summary>
        /// 此实体的唯一标识符
        /// </summary>
        TKey Id { get; }
    }

}
