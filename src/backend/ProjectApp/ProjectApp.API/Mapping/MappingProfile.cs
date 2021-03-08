using AutoMapper;
using ProjectApp.Model.Binding;
using ProjectApp.Model.Entities.User;
using ProjectApp.Model.DTO;
using ProjectApp.BindingModel;
using ProjectApp.Domain.Entities;

namespace ProjectApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // BindingModel to Entity
            CreateMap<UserProfileBindingModel, UserProfile>();
            CreateMap<TextBindingModel, Text>();

            // Entity to DTO
            CreateMap<UserProfile, UserProfileDTO>();
        }
    }
}