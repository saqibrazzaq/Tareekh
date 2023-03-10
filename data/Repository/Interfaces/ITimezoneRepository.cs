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
    public interface ITimezoneRepository : IRepositoryBase<Timezone>
    {
        PagedList<Timezone> Search(TimezoneReqSearch dto, bool trackChanges);
    }
}
