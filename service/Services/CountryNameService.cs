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

        public CountryNameRes? Create(CountryNameReqEdit dto)
        {
            var entity = _mapper.Map<CountryName>(dto);
            _repositoryManager.CountryNameRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<CountryNameRes>(entity);
        }

        public void Delete(int countryNameId)
        {
            var entity = FindCountryNameIfExists(countryNameId, true);
            _repositoryManager.CountryNameRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public CountryNameRes? Get(int countryNameId)
        {
            var entity = FindCountryNameIfExists(countryNameId, false);
            return _mapper.Map<CountryNameRes>(entity);
        }

        private CountryName? FindCountryNameIfExists(int countryNameId, bool trackChanges)
        {
            var entity = _repositoryManager.CountryNameRepository.FindByCondition(
                x => x.CountryNameId == countryNameId,
                trackChanges,
                include: i => i
                    .Include(x => x.Language)
                    .Include(x => x.Country)
                    )
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No Country Name found with id " + countryNameId); }

            return entity;
        }

        public CountryNameRes? Update(int countryNameId, CountryNameReqEdit dto)
        {
            var entity = FindCountryNameIfExists(countryNameId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<CountryNameRes>(entity);
        }

        public ApiOkPagedResponse<IEnumerable<CountryNameRes>, MetaData> Search(CountryNameReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CountryNameRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<CountryNameRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<CountryNameRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public int Count(int countryId)
        {
            return _repositoryManager.CountryNameRepository.FindByCondition(
                x => x.CountryId == countryId,
                false)
                .Count();
        }

        public bool AnyByLanguage(int languageId)
        {
            return _repositoryManager.CountryNameRepository.FindByCondition(
                x => x.LanguageId == languageId,
                false)
                .Any();
        }

        public int CountByLanguage(int languageId)
        {
            return _repositoryManager.CountryNameRepository.FindByCondition(
                x => x.LanguageId == languageId,
                false)
                .Count();
        }
    }
}
