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
        Timezone? Get(int timezoneId);
        Timezone? Create(Timezone timezone);
        Timezone? Update(int timezoneId, Timezone timezone);
        void Delete(int timezoneId);
        int Count();
        ApiOkPagedResponse<IEnumerable<Timezone>, MetaData>
            Search(TimezoneReqSearch dto);
    }
}
