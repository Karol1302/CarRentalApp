using ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels;
using ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels.ApplicationUsers;
using ATHCarRentNetworkSystem.Models;
using ATHCarRentNetworkSystem.ViewModels;
using AutoMapper;

namespace ATHCarRentNetworkSystem.Mapper
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Samochod, SamochodsIndexViewModel>()
                .ForMember(r => r.TypSamochodu, opt => opt.MapFrom<string>(r => r.TypSamochodu.Nazwa));
            CreateMap<Samochod, SamochodsDetailsViewModel>()
                .ForMember(r => r.TypSamochodu, opt => opt.MapFrom<string>(r => r.TypSamochodu.Nazwa));
           
            CreateMap<Samochod, SamochodsEditViewModel>();
            CreateMap<SamochodsEditViewModel, Samochod>();

            CreateMap<Wypozyczenie, WypozyczenieIndexViewModel>()
                .ForMember(r => r.Samochod, opt => opt.MapFrom<string>(r => r.Samochod.Nazwa))
                .ForMember(r=> r.status, opt=> opt.MapFrom<string>(r => r.status.ToString()));

            CreateMap<Wypozyczenie, WypozyczenieEditViewModel>();
            CreateMap<WypozyczenieEditViewModel, Wypozyczenie>();

            CreateMap<Wypozyczenie, WypozyczenieDetailsViewModel>()
                .ForMember(r => r.status, opt => opt.MapFrom<string>(r => r.status.ToString()))
                .ForMember(r => r.Samochod, opt => opt.MapFrom<string>(r => r.Samochod.Nazwa));

            CreateMap<ApplicationUser, ApplicationUsersIndexViewModel>()
                .ForMember(r => r.Wypozyczalnia, opt => opt.MapFrom<string>(r => r.Wypozyczalnia.Name));

            CreateMap<ApplicationUser, ApplicationUsersDetailsViewModel>()
                .ForMember(r => r.Wypozyczalnia, opt => opt.MapFrom<string>(r => r.Wypozyczalnia.Name));

            CreateMap<ApplicationUser, ApplicationUsersEditViewModel>();
            CreateMap<ApplicationUsersEditViewModel, ApplicationUser>();
        }
    }
}
