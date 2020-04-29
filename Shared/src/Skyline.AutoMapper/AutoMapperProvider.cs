using AutoMapper;
using System;

namespace Skyline.AutoMapper
{
    /// <summary>
    /// AutoMapper提供者
    /// </summary>
    public class AutoMapperProvider
    {
        private readonly MapperConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// AutoMapper提供者
        /// </summary>
        /// <param name="mapperConfiguration">mapper配置</param>
        public AutoMapperProvider(MapperConfiguration mapperConfiguration)
        {
            _configuration = mapperConfiguration;
            _mapper = _configuration.CreateMapper();
        }

        /// <summary>
        /// 将<paramref name="source"/>映射为<typeparamref name="TDestination"/>类型对象
        /// </summary>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public TDestination MapTo<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 将<paramref name="source"/>映射为<paramref name="destination"/>
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns></returns>
        public TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
