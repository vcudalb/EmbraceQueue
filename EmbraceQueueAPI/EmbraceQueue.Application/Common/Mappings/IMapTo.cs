using AutoMapper;

namespace EmbraceQueue.Application.Common.Mappings
{
    public interface IMapTo<T>
    {
        void MapTo(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
