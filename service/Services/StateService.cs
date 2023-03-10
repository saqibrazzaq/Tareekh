using AutoMapper;
using data.Repository.Interfaces;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.EntityFrameworkCore;
using service.Services.Interfaces;

namespace service.Services
{
    public class StateService : IStateService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public StateService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public int Count()
        {
            return _repositoryManager.StateRepository.FindAll(false)
                .Count();
        }

        public int Count(int countryId)
        {
            return _repositoryManager.StateRepository.FindByCondition(
                x => x.CountryId == countryId,
                false)
                .Count();
        }

        public State? Create(State state)
        {
            _repositoryManager.StateRepository.Create(state);
            _repositoryManager.Save();
            return state;
        }

        public void Delete(int stateId)
        {
            var entity = FindStateIfExists(stateId, true);
            _repositoryManager.StateRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public State? Get(int stateId)
        {
            return FindStateIfExists(stateId, false);
        }

        private State? FindStateIfExists(int stateId, bool trackChanges)
        {
            var entity = _repositoryManager.StateRepository.FindByCondition(
                x => x.StateId == stateId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No State found with id " + stateId); }

            return entity;
        }

        public State? GetBySlug(string slug)
        {
            var entity = _repositoryManager.StateRepository.FindByCondition(
                x => x.Slug == slug,
                false,
                include: i => i.Include(x => x.Country))
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No state found with slug " + slug); }

            return entity;
        }

        public ApiOkPagedResponse<IEnumerable<State>, MetaData> Search(StateReqSearch dto)
        {
            var pagedEntities = _repositoryManager.StateRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<State>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public State? Update(int stateId, State state)
        {
            var entity = FindStateIfExists(stateId, true);
            _mapper.Map(state, entity);
            _repositoryManager.Save();
            return entity;
        }
    }
}
