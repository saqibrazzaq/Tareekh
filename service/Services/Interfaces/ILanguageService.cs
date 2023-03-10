using dto.dtos;
using dto.Paging;
using entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.Interfaces
{
    public interface ILanguageService
    {
        Language? Get(int languageId);
        Language? Create(Language language);
        Language? Update(int languageId, Language language);
        void Delete(int languageId);
        int Count();
        ApiOkPagedResponse<IEnumerable<Language>, MetaData>
            Search(LanguageReqSearch dto);
    }
}
