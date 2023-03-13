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
        StateNameRes? Get(int stateNameId);
        StateNameRes? Create(StateNameReqEdit dto);
        StateNameRes? Update(int stateNameId, StateNameReqEdit dto);
        void Delete(int stateNameId);
        int Count(int stateId);
        ApiOkPagedResponse<IEnumerable<StateNameRes>, MetaData>
            Search(StateNameReqSearch dto);
    }
}
