using AutoMapper;
using Psinder.Api.Data;
using Psinder.Api.Models;

namespace MovieNotice.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Pet, PetModel>();
            CreateMap<Shelter, ShelterModel>();
            CreateMap<ShelterModel, Shelter>();
            CreateMap<User, UserModel>();      
            

        }
    }
}
