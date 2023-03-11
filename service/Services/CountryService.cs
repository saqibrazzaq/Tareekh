﻿using AutoMapper;
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

        public CountryRes? Create(CountryReqEdit dto)
        {
            var entity = _mapper.Map<Country>(dto);
            _repositoryManager.CountryRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<CountryRes>(entity);
        }

        public void Delete(int countryId)
        {
            var entity = FindCountryIfExists(countryId, true);
            _repositoryManager.CountryRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public CountryRes? Get(int countryId)
        {
            var entity = FindCountryIfExists(countryId, false);
            return _mapper.Map<CountryRes>(entity);
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

        public CountryRes? GetBySlug(string slug)
        {
            var entity = _repositoryManager.CountryRepository.FindByCondition(
                x => x.Slug == slug,
                false)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No country found with id " + slug); }

            return _mapper.Map<CountryRes>(entity);
        }

        public ApiOkPagedResponse<IEnumerable<CountryRes>, MetaData> Search(CountryReqSearch dto)
        {
            var pagedEntities = _repositoryManager.CountryRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<CountryRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<CountryRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public CountryRes? Update(int countryId, CountryReqEdit dto)
        {
            var entity = FindCountryIfExists(countryId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<CountryRes>(entity);
        }
    }
}
