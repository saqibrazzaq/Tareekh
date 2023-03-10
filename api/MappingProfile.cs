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
            CreateMap<Country, CountryRes>();
            CreateMap<CountryReqEdit, Country>();

            // CountryName
            CreateMap<CountryName, CountryNameRes>();
            CreateMap<CountryNameReqEdit, CountryName>();

            // State
            CreateMap<State, StateRes>();
            CreateMap<StateReqEdit, State>();

            // StateName
            CreateMap<StateName, StateNameRes>();
            CreateMap<StateNameReqEdit, StateName>();

            // City
            CreateMap<City, CityRes>();
            CreateMap<CityReqEdit, City>();

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
