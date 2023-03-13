using AutoMapper;
using data.Repository.Interfaces;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.EntityFrameworkCore;
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

        public CityNameRes? Create(CityNameReqEdit dto)
        {
            var entity = _mapper.Map<CityName>(dto);
            _repositoryManager.CityNameRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<CityNameRes>(entity);
        }

        public void Delete(int cityNameId)
        {
            var entity = FindCityNameIfExists(cityNameId, true);
            _repositoryManager.CityNameRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public CityNameRes? Get(int cityNameId)
        {
            var entity = FindCityNameIfExists(cityNameId, false);
            return _mapper.Map<CityNameRes>(entity);
        }

        private CityName? FindCityNameIfExists(int cityNameId, bool trackChanges)
        {
            var entity = _repositoryManager.CityNameRepository.FindByCondition(
                x => x.CityNameId == cityNameId,
                trackChanges,
                include: i => i.Include(x => x.Language))
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No City Name found with id " + cityNameId); }

            return entity;
        }

        public CityNameRes? Update(int cityNameId, CityNameReqEdit dto)
        {
            var entity = FindCityNameIfExists(cityNameId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<CityNameRes>(entity);
        }

        public ApiOkPagedResponse<IEnumerable<CityNameRes>, MetaData> Search(CityNameReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CityNameRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<CityNameRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<CityNameRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public int Count(int cityId)
        {
            return _repositoryManager.CityNameRepository.FindAll(false)
                .Count();
        }
    }
}
