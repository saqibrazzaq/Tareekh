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

        public CityRes? Get(int cityId)
        {
            var entity = FindCityIfExists(cityId, false);
            return _mapper.Map<CityRes>(entity);
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

        public CityRes? GetBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return null;

            var entity = _repositoryManager.CityRepository.FindByCondition(
                x => x.Slug == slug,
                false,
                include: i => i.Include(x => x.State.Country))
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No city found with slug " + slug); }

            return _mapper.Map<CityRes>(entity);
        }

        public ApiOkPagedResponse<IEnumerable<CityRes>, MetaData> Search(CityReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CityRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<CityRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<CityRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public CityRes? Create(CityReqEdit dto)
        {
            var entity = _mapper.Map<City>(dto);
            _repositoryManager.CityRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<CityRes>(entity);
        }

        public CityRes? Update(int cityId, CityReqEdit dto)
        {
            var entity = FindCityIfExists(cityId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<CityRes>(entity);
        }

        public void Delete(int cityId)
        {
            var entity = FindCityIfExists(cityId, true);
            _repositoryManager.CityRepository.Delete(entity);
            _repositoryManager.Save();
        }
    }
}
