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
    public class TimezoneService : ITimezoneService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        public TimezoneService(IRepositoryManager repositoryManager,
            IMapper mapper,
            ICityService cityService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _cityService = cityService;
        }

        public int Count()
        {
            return _repositoryManager.TimezoneRepository.FindAll(false)
                .Count();
        }

        public TimezoneRes? Create(TimezoneReqEdit dto)
        {
            var entity = _mapper.Map<Timezone>(dto);
            _repositoryManager.TimezoneRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<TimezoneRes>(entity);
        }

        public void Delete(int timezoneId)
        {
            ValidateForDelete(timezoneId);

            var entity = FindTimezoneIfExists(timezoneId, true);
            _repositoryManager.TimezoneRepository.Delete(entity);
            _repositoryManager.Save();
        }

        private void ValidateForDelete(int timezoneId)
        {
            var anyCityByTimezone = _cityService.AnyByTimezone(timezoneId);
            if (anyCityByTimezone)
                throw new Exception("Cannot delete timezone, it is used in cities.");
        }

        public TimezoneRes? Get(int timezoneId)
        {
            var entity = FindTimezoneIfExists(timezoneId, false);
            return _mapper.Map<TimezoneRes>(entity);
        }

        private Timezone? FindTimezoneIfExists(int timezoneId, bool trackChanges)
        {
            var entity = _repositoryManager.TimezoneRepository.FindByCondition(
                x => x.TimezoneId == timezoneId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No Timezone found with id " + timezoneId); }

            return entity;
        }

        public ApiOkPagedResponse<IEnumerable<TimezoneRes>, MetaData> Search(TimezoneReqSearch dto)
        {
            var pagedEntities = _repositoryManager.TimezoneRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<TimezoneRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<TimezoneRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public TimezoneRes? Update(int timezoneId, TimezoneReqEdit dto)
        {
            var entity = FindTimezoneIfExists(timezoneId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<TimezoneRes>(entity);
        }
    }
}
