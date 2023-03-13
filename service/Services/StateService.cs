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
        private readonly ICityService _cityService;
        private readonly IStateNameService _stateNameService;
        public StateService(IRepositoryManager repositoryManager,
            IMapper mapper,
            ICityService cityService,
            IStateNameService stateNameService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _cityService = cityService;
            _stateNameService = stateNameService;
        }

        public int Count()
        {
            return _repositoryManager.StateRepository.FindAll(false)
                .Count();
        }

        public int CountByCountry(int countryId)
        {
            return _repositoryManager.StateRepository.FindByCondition(
                x => x.CountryId == countryId,
                false)
                .Count();
        }

        public StateRes? Create(StateReqEdit dto)
        {
            var entity = _mapper.Map<State>(dto);
            _repositoryManager.StateRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<StateRes>(entity);
        }

        public void Delete(int stateId)
        {
            ValidateForDelete(stateId);

            var entity = FindStateIfExists(stateId, true);
            _repositoryManager.StateRepository.Delete(entity);
            _repositoryManager.Save();
        }

        private void ValidateForDelete(int stateId)
        {
            var cityCount = _cityService.Count(stateId);
            if (cityCount > 0)
                throw new Exception(string.Format("Cannot delete State, it has {0} cities.", cityCount));

            var stateNameCount = _stateNameService.Count(stateId);
            if (stateNameCount > 0)
                throw new Exception(string.Format("Cannot delete state, it has {0} names.", stateNameCount));
        }

        public StateRes? Get(int stateId)
        {
            var entity = FindStateIfExists(stateId, false);
            return _mapper.Map<StateRes>(entity);
        }

        private State? FindStateIfExists(int stateId, bool trackChanges)
        {
            var entity = _repositoryManager.StateRepository.FindByCondition(
                x => x.StateId == stateId,
                trackChanges,
                include: i => i.Include(x => x.Country))
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No State found with id " + stateId); }

            return entity;
        }

        public StateRes? GetBySlug(string slug)
        {
            var entity = _repositoryManager.StateRepository.FindByCondition(
                x => x.Slug == slug,
                false,
                include: i => i.Include(x => x.Country))
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No state found with slug " + slug); }

            return _mapper.Map<StateRes>(entity);
        }

        public ApiOkPagedResponse<IEnumerable<StateRes>, MetaData> Search(StateReqSearch dto)
        {
            var pagedEntities = _repositoryManager.StateRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<StateRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<StateRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public StateRes? Update(int stateId, StateReqEdit dto)
        {
            var entity = FindStateIfExists(stateId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<StateRes>(entity);
        }

        public bool AnyByCountry(int countryId)
        {
            return _repositoryManager.StateRepository.FindByCondition(
                x => x.CountryId == countryId,
                false)
                .Any();
        }
    }
}
