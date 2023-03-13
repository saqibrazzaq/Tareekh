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
    public class LanguageService : ILanguageService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public LanguageService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public int Count()
        {
            return _repositoryManager.LanguageRepository.FindAll(false)
                .Count();
        }

        public LanguageRes? Create(LanguageReqEdit dto)
        {
            var entity = _mapper.Map<Language>(dto);
            _repositoryManager.LanguageRepository.Create(entity);
            _repositoryManager.Save();
            return _mapper.Map<LanguageRes>(entity);
        }

        public void Delete(int languageId)
        {
            var entity = FindLanguageIfExists(languageId, true);
            _repositoryManager.LanguageRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public LanguageRes? Get(int languageId)
        {
            var entity = FindLanguageIfExists(languageId, false);
            return _mapper.Map<LanguageRes>(entity);
        }

        private Language? FindLanguageIfExists(int languageId, bool trackChanges)
        {
            var entity = _repositoryManager.LanguageRepository.FindByCondition(
                x => x.LanguageId == languageId,
                trackChanges)
                .FirstOrDefault();
            if (entity == null) { throw new Exception("No Language found with id " + languageId); }

            return entity;
        }

        public ApiOkPagedResponse<IEnumerable<LanguageRes>, MetaData> Search(LanguageReqSearch dto)
        {
            var pagedEntities = _repositoryManager.LanguageRepository.
                Search(dto, false);
            var dtos = _mapper.Map<IEnumerable<LanguageRes>>(pagedEntities);
            return new ApiOkPagedResponse<IEnumerable<LanguageRes>, MetaData>(dtos,
                pagedEntities.MetaData);
        }

        public LanguageRes? Update(int languageId, LanguageReqEdit dto)
        {
            var entity = FindLanguageIfExists(languageId, true);
            _mapper.Map(dto, entity);
            _repositoryManager.Save();
            return _mapper.Map<LanguageRes>(entity);
        }
    }
}
