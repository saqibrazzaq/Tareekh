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
        public TimezoneService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public int Count()
        {
            return _repositoryManager.TimezoneRepository.FindAll(false)
                .Count();
        }

        public Timezone? Create(Timezone timezone)
        {
            _repositoryManager.TimezoneRepository.Create(timezone);
            _repositoryManager.Save();
            return timezone;
        }

        public void Delete(int timezoneId)
        {
            var entity = FindTimezoneIfExists(timezoneId, true);
            _repositoryManager.TimezoneRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public Timezone? Get(int timezoneId)
        {
            return FindTimezoneIfExists(timezoneId, false);
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

        public ApiOkPagedResponse<IEnumerable<Timezone>, MetaData> Search(TimezoneReqSearch dto)
        {
            var pagedEntities = _repositoryManager.TimezoneRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<Timezone>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public Timezone? Update(int timezoneId, Timezone timezone)
        {
            var entity = FindTimezoneIfExists(timezoneId, true);
            _mapper.Map(timezone, entity);
            _repositoryManager.Save();
            return entity;
        }
    }
}
