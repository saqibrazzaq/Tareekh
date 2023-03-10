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
    public interface IStateNameService
    {
        StateName? Get(int stateNameId);
        StateName? Create(StateName stateName);
        StateName? Update(int stateNameId, StateName stateName);
        void Delete(int stateNameId);
        int Count(int stateId);
        ApiOkPagedResponse<IEnumerable<StateName>, MetaData>
            Search(StateNameReqSearch dto);
    }
}
