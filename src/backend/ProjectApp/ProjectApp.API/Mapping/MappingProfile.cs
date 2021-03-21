using AutoMapper;
using ProjectApp.Model.Binding;
using ProjectApp.Model.Entities.User;
using ProjectApp.Model.DTO;
using ProjectApp.Model.Entities;
using ProjectApp.DTO.Text;

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
            CreateMap<Text, TextFromDbDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.TextContent));
        }
    }
}