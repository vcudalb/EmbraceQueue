using AutoMapper;
using System.Collections.Generic;

namespace EmbraceQueue.Application.Common.Mappings
{
    public static class AutomapperExtensions
    {
        public static IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(this IEnumerable<TSource> enumerable, IMapper mapper)
        {
            return mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(enumerable);
        }
    }
}
