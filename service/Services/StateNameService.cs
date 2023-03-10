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

        public StateName? Create(StateName stateName)
        {
            _repositoryManager.StateNameRepository.Create(stateName);
            _repositoryManager.Save();
            return stateName;
        }

        public void Delete(int stateNameId)
        {
            var entity = FindStateNameIfExists(stateNameId, true);
            _repositoryManager.StateNameRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public StateName? Get(int stateNameId)
        {
            return FindStateNameIfExists(stateNameId, false);
        }

        private StateName? FindStateNameIfExists(int stateNameId, bool trackChanges)
        {
            var entity = _repositoryManager.StateNameRepository.FindByCondition(
                x => x.StateNameId == stateNameId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No State Name found with id " + stateNameId); }

            return entity;
        }

        public StateName? Update(int stateNameId, StateName stateName)
        {
            var entity = FindStateNameIfExists(stateNameId, true);
            _mapper.Map(stateName, entity);
            _repositoryManager.Save();
            return entity;
        }

        public ApiOkPagedResponse<IEnumerable<StateName>, MetaData> Search(StateNameReqSearch dto)
        {
            var pagedEntities = _repositoryManager.StateNameRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<StateName>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public int Count(int stateId)
        {
            return _repositoryManager.StateNameRepository.FindByCondition(
                x => x.StateId == stateId,
                false)
                .Count();
        }
    }
}
