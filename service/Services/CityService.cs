using AutoMapper;
using data.Repository.Interfaces;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.EntityFrameworkCore;
using service.Services.Interfaces;

namespace service.Services
{
    public class CityService : ICityService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CityService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public int Count()
        {
            return _repositoryManager.CityRepository.FindAll(false)
                .Count();
        }

        public int Count(int stateId)
        {
            return _repositoryManager.CityRepository.FindByCondition(
                x => x.StateId == stateId,
                false)
                .Count();
        }

        public City? Get(int cityId)
        {
            return FindCityIfExists(cityId, false);
        }

        private City? FindCityIfExists(int cityId, bool trackChanges)
        {
            var entity = _repositoryManager.CityRepository.FindByCondition(
                x => x.CityId == cityId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No City found with id " + cityId); }

            return entity;
        }

        public City? GetBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return null;

            var entity = _repositoryManager.CityRepository.FindByCondition(
                x => x.Slug == slug,
                false,
                include: i => i.Include(x => x.State.Country))
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No city found with slug " + slug); }

            return entity;
        }

        public ApiOkPagedResponse<IEnumerable<City>, MetaData> Search(CityReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CityRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<City>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public City? Create(City city)
        {
            _repositoryManager.CityRepository.Create(city);
            _repositoryManager.Save();
            return city;
        }

        public City? Update(int cityId, City city)
        {
            var entity = FindCityIfExists(cityId, true);
            _mapper.Map(city, entity);
            _repositoryManager.Save();
            return entity;
        }

        public void Delete(int cityId)
        {
            var entity = FindCityIfExists(cityId, true);
            _repositoryManager.CityRepository.Delete(entity);
            _repositoryManager.Save();
        }
    }
}
