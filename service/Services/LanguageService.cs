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

        public Language? Create(Language language)
        {
            _repositoryManager.LanguageRepository.Create(language);
            _repositoryManager.Save();
            return language;
        }

        public void Delete(int languageId)
        {
            var entity = FindLanguageIfExists(languageId, true);
            _repositoryManager.LanguageRepository.Delete(entity);
            _repositoryManager.Save();
        }

        public Language? Get(int languageId)
        {
            return FindLanguageIfExists(languageId, false);
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

        public ApiOkPagedResponse<IEnumerable<Language>, MetaData> Search(LanguageReqSearch dto)
        {
            var pagedEntities = _repositoryManager.LanguageRepository.
                Search(dto, false);
            return new ApiOkPagedResponse<IEnumerable<Language>, MetaData>(pagedEntities,
                pagedEntities.MetaData);
        }

        public Language? Update(int languageId, Language language)
        {
            var entity = FindLanguageIfExists(languageId, true);
            _mapper.Map(language, entity);
            _repositoryManager.Save();
            return entity;
        }
    }
}
