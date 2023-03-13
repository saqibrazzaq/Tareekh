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
    public class StateNameService : IStateNameService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public StateNameService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public StateNameRes? Create(StateNameReqEdit dto)
        {
            var entity = _mapper.Map<StateName>(dto);
            _repositoryManager.StateNameRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<StateNameRes>(entity);
        }

        public void Delete(int stateNameId)
        {
            var entity = FindStateNameIfExists(stateNameId, true);
            _repositoryManager.StateNameRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public StateNameRes? Get(int stateNameId)
        {
            var entity = FindStateNameIfExists(stateNameId, false);
            return _mapper.Map<StateNameRes>(entity);
        }

        private StateName? FindStateNameIfExists(int stateNameId, bool trackChanges)
        {
            var entity = _repositoryManager.StateNameRepository.FindByCondition(
                x => x.StateNameId == stateNameId,
                trackChanges,
                include: i => i
                    .Include(x => x.Language)
                    .Include(x => x.State.Country)
                    )
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No State Name found with id " + stateNameId); }

            return entity;
        }

        public StateNameRes? Update(int stateNameId, StateNameReqEdit dto)
        {
            var entity = FindStateNameIfExists(stateNameId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<StateNameRes>(entity);
        }

        public ApiOkPagedResponse<IEnumerable<StateNameRes>, MetaData> Search(StateNameReqSearch dto)
        {
            var pagedEntities = _repositoryManager.StateNameRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<StateNameRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<StateNameRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public int Count(int stateId)
        {
            return _repositoryManager.StateNameRepository.FindByCondition(
                x => x.StateId == stateId,
                false)
                .Count();
        }

        public bool AnyByLanguage(int languageId)
        {
            return _repositoryManager.StateNameRepository.FindByCondition(
                x => x.LanguageId == languageId,
                false)
                .Any();
        }

        public int CountByLanguage(int languageId)
        {
            return _repositoryManager.StateNameRepository.FindByCondition(
                x => x.LanguageId == languageId,
                false)
                .Count();
        }
    }
}
