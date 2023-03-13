using AutoMapper;
using dto.dtos;
using entity.Entities;

namespace api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Country
            CreateMap<Country, CountryRes>()
                .ForMember(dst => dst.Slug, opt => opt.MapFrom(src => src.Slug.ToLower()));
            CreateMap<CountryReqEdit, Country>()
                .ForMember(dst => dst.Slug, opt => opt.MapFrom(src => src.Slug.ToLower()));

            // CountryName
            CreateMap<CountryName, CountryNameRes>();
            CreateMap<CountryNameReqEdit, CountryName>();

            // State
            CreateMap<State, StateRes>()
                .ForMember(dst => dst.Slug, opt => opt.MapFrom(src => src.Slug.ToLower()));
            CreateMap<StateReqEdit, State>()
                .ForMember(dst => dst.Slug, opt => opt.MapFrom(src => src.Slug.ToLower()));

            // StateName
            CreateMap<StateName, StateNameRes>();
            CreateMap<StateNameReqEdit, StateName>();

            // City
            CreateMap<City, CityRes>()
                .ForMember(dst => dst.Slug, opt => opt.MapFrom(src => src.Slug.ToLower()));
            CreateMap<CityReqEdit, City>()
                .ForMember(dst => dst.Slug, opt => opt.MapFrom(src => src.Slug.ToLower()));

            // CityName
            CreateMap<CityName, CityNameRes>();
            CreateMap<CityNameReqEdit, CityName>();

            // Language
            CreateMap<Language, LanguageRes>();
            CreateMap<LanguageReqEdit, Language>();

            // Timezone
            CreateMap<Timezone, TimezoneRes>();
            CreateMap<TimezoneReqEdit, Timezone>();
        }
    }
}
