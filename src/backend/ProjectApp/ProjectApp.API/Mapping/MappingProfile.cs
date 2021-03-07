using AutoMapper;
using ProjectApp.Model.Binding;
using ProjectApp.Model.Entities.User;
using ProjectApp.Model.DTO;

namespace ProjectApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // BindingModel to Entity
            CreateMap<UserProfileBindingModel, UserProfile>();

            // Entity to DTO
            CreateMap<UserProfile, UserProfileDTO>();
        }
    }
}