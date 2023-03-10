using AutoMapper;
using data.Repository.Interfaces;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services
{
    public class CityNameService : ICityNameService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CityNameService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public CityName? Create(CityName cityName)
        {
            _repositoryManager.CityNameRepository.Create(cityName);
            _repositoryManager.Save();
            return cityName;
        }

        public void Delete(int cityNameId)
        {
            var entity = FindCityNameIfExists(cityNameId, true);
            _repositoryManager.CityNameRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public CityName? Get(int cityNameId)
        {
            return FindCityNameIfExists(cityNameId, false);
        }

        private CityName? FindCityNameIfExists(int cityNameId, bool trackChanges)
        {
            var entity = _repositoryManager.CityNameRepository.FindByCondition(
                x => x.CityNameId == cityNameId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No City Name found with id " + cityNameId); }

            return entity;
        }

        public CityName? Update(int cityNameId, CityName cityName)
        {
            var entity = FindCityNameIfExists(cityNameId, true);
            _mapper.Map(cityName, entity);
            _repositoryManager.Save();
            return cityName;
        }

        public ApiOkPagedResponse<IEnumerable<CityName>, MetaData> Search(CityNameReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CityNameRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<CityName>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public int Count(int cityId)
        {
            return _repositoryManager.CityNameRepository.FindAll(false)
                .Count();
        }
    }
}
