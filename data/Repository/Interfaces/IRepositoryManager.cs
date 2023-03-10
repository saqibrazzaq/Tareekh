namespace data.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        ICountryRepository CountryRepository { get; }
        ICountryNameRepository CountryNameRepository { get; }
        IStateRepository StateRepository { get; }
        IStateNameRepository StateNameRepository { get; }
        ICityRepository CityRepository { get; }
        ICityNameRepository CityNameRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        ITimezoneRepository TimezoneRepository { get; }
        void Save();
    }
}
