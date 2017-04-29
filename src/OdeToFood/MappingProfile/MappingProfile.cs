using AutoMapper;
using OdeToFood.Entities;
using OdeToFood.ViewModels;

namespace OdeToFood.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            InitializeMapping();
        }

        private void InitializeMapping()
        {
            CreateMap<RestaurantEditViewModel, Restaurant>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Restaurant, RestaurantEditViewModel>();

            CreateMap<RegisterUserViewModel, User>()
                .ForMember(x=>x.UserName, opt=>opt.MapFrom(x=>x.UserName))
                .ForAllOtherMembers(opt=>opt.Ignore());
        }
    }
}
