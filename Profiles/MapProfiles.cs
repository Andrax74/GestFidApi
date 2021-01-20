using GestFidApi.Models;
using AutoMapper;
using GestFidApi.Dtos;

namespace GestFidApi.Profiles
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Clienti, ClientiDto>()
            .ForMember
            (
                dest => dest.CodFid,
                opt => opt.MapFrom(src => src.CodFid.Trim())
            )
            .ForMember
            (
                dest => dest.Transazioni,
                opt => opt.MapFrom(src => src.transaz)
            )
            .ForMember
            (
                dest => dest.IdAvatar,
                opt => opt.MapFrom(src => src.IdAvatar.Trim())
            );

            CreateMap<Transazioni, TransazDto>();
        }
    }
}