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
    public interface ICityNameRepository : IRepositoryBase<CityName>
    {
        PagedList<CityName> Search(CityNameReqSearch dto, bool trackChanges);
    }
}
