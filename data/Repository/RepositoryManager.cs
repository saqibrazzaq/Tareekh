using data.Repository.Interfaces;

namespace data.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<ICountryNameRepository> _countryNameRepository;
        private readonly Lazy<IStateRepository> _stateRepository;
        private readonly Lazy<IStateNameRepository> _stateNameRepository;
        private readonly Lazy<ICityRepository> _cityRepository;
        private readonly Lazy<ICityNameRepository> _cityNameRepository;
        private readonly Lazy<ILanguageRepository> _languageRepository;
        private readonly Lazy<ITimezoneRepository> _timezoneRepository;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;

            // Initialize all the repositories
            _countryRepository = new Lazy<ICountryRepository>(() =>
                new CountryRepository(context));
            _countryNameRepository = new Lazy<ICountryNameRepository>(() =>
                new CountryNameRepository(context));
            _stateRepository = new Lazy<IStateRepository>(() =>
                new StateRepository(context));
            _stateNameRepository = new Lazy<IStateNameRepository>(() =>
                new StateNameRepository(context));
            _cityRepository = new Lazy<ICityRepository>(() =>
                new CityRepository(context));
            _cityNameRepository = new Lazy<ICityNameRepository>(() =>
                new CityNameRepository(context));
            _languageRepository = new Lazy<ILanguageRepository>(() =>
                new LanguageRepository(context));
            _timezoneRepository = new Lazy<ITimezoneRepository>(() =>
                new TimezoneRepository(context));
        }

        public ICountryRepository CountryRepository => _countryRepository.Value;
        public ICountryNameRepository CountryNameRepository => _countryNameRepository.Value;
        public IStateRepository StateRepository => _stateRepository.Value;
        public IStateNameRepository StateNameRepository => _stateNameRepository.Value;
        public ICityRepository CityRepository => _cityRepository.Value;
        public ICityNameRepository CityNameRepository => _cityNameRepository.Value;
        public ILanguageRepository LanguageRepository => _languageRepository.Value;
        public ITimezoneRepository TimezoneRepository => _timezoneRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
