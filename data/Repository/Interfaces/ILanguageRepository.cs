using dto.dtos;
using dto.Paging;
using entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository.Interfaces
{
    public interface ILanguageRepository : IRepositoryBase<Language>
    {
        PagedList<Language> Search(LanguageReqSearch dto, bool trackChanges);
    }
}
