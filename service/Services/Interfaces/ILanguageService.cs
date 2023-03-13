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
        LanguageRes? Get(int languageId);
        LanguageRes? Create(LanguageReqEdit dto);
        LanguageRes? Update(int languageId, LanguageReqEdit dto);
        void Delete(int languageId);
        int Count();
        ApiOkPagedResponse<IEnumerable<LanguageRes>, MetaData>
            Search(LanguageReqSearch dto);
    }
}
