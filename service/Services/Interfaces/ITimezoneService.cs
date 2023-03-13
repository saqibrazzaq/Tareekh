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
    public interface ITimezoneService
    {
        TimezoneRes? Get(int timezoneId);
        TimezoneRes? Create(TimezoneReqEdit dto);
        TimezoneRes? Update(int timezoneId, TimezoneReqEdit dto);
        void Delete(int timezoneId);
        int Count();
        ApiOkPagedResponse<IEnumerable<TimezoneRes>, MetaData>
            Search(TimezoneReqSearch dto);
    }
}
