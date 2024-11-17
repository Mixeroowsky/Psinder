using AutoMapper;
using Psinder.Server.Dtos;
using Psinder.Server.Entities;

namespace Psinder.Server.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pet, PetDto>();
            CreateMap<PetDto, Pet>();
            CreateMap<Shelter, ShelterDto>();
            CreateMap<ShelterDto, Shelter>();
            CreateMap<User, UserDto>();
        }
    }
}
