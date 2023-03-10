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
    public class CountryNameService : ICountryNameService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CountryNameService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public CountryName? Create(CountryName countryName)
        {
            _repositoryManager.CountryNameRepository.Create(countryName);
            _repositoryManager.Save();
            return countryName;
        }

        public void Delete(int countryNameId)
        {
            var entity = FindCountryNameIfExists(countryNameId, true);
            _repositoryManager.CountryNameRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public CountryName? Get(int countryNameId)
        {
            return FindCountryNameIfExists(countryNameId, false);
        }

        private CountryName? FindCountryNameIfExists(int countryNameId, bool trackChanges)
        {
            var entity = _repositoryManager.CountryNameRepository.FindByCondition(
                x => x.CountryNameId == countryNameId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No Country Name found with id " + countryNameId); }

            return entity;
        }

        public CountryName? Update(int countryNameId, CountryName countryName)
        {
            var entity = FindCountryNameIfExists(countryNameId, true);
            _mapper.Map(countryName, entity);
            _repositoryManager.Save();
            return entity;
        }

        public ApiOkPagedResponse<IEnumerable<CountryName>, MetaData> Search(CountryNameReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CountryNameRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<CountryName>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public int Count(int countryId)
        {
            return _repositoryManager.CountryNameRepository.FindByCondition(
                x => x.CountryId == countryId,
                false)
                .Count();
        }
    }
}
