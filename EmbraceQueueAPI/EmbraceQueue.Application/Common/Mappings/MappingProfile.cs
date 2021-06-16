using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace EmbraceQueue.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var genericTypes = new Type[] { typeof(IMapFrom<>), typeof(IMapTo<>) };

            var types = assembly
                .GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && genericTypes.Contains(i.GetGenericTypeDefinition())))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var mapFromMethodInfo = type.GetMethod("MapFrom") ?? type.GetInterface("IMapFrom`1")?.GetMethod("MapFrom");
                mapFromMethodInfo?.Invoke(instance, new object[] { this });

                var mapToMethodInfo = type.GetMethod("MapTo") ?? type.GetInterface("IMapTo`1")?.GetMethod("MapTo");
                mapToMethodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
