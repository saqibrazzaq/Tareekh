using AutoMapper;
using data.Repository.Interfaces;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using service.Services.Interfaces;

namespace service.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CountryService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public int Count()
        {
            return _repositoryManager.CountryRepository.FindAll(false)
                .Count();
        }

        public Country? Create(Country country)
        {
            _repositoryManager.CountryRepository.Create(country);
            _repositoryManager.Save();
            return country;
        }

        public void Delete(int countryId)
        {
            var entity = FindCountryIfExists(countryId, true);
            _repositoryManager.CountryRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public Country? Get(int countryId)
        {
            return FindCountryIfExists(countryId, false);
        }

        private Country? FindCountryIfExists(int countryId, bool trackChanges)
        {
            var entity = _repositoryManager.CountryRepository.FindByCondition(
                x => x.CountryId == countryId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No Country found with id " + countryId); }

            return entity;
        }

        public Country? GetBySlug(string slug)
        {
            var entity = _repositoryManager.CountryRepository.FindByCondition(
                x => x.Slug == slug,
                false)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No country found with id " + slug); }

            return entity;
        }

        public ApiOkPagedResponse<IEnumerable<Country>, MetaData> Search(CountryReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CountryRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<Country>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public Country? Update(int countryId, Country country)
        {
            var entity = FindCountryIfExists(countryId, true);
            _mapper.Map(country, entity);
            _repositoryManager.Save();
            return entity;
        }
    }
}
