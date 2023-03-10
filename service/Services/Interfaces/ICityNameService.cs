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
    public interface ICityNameService
    {
        CityName? Get(int cityNameId);
        CityName? Create(CityName cityName);
        CityName? Update(int cityNameId, CityName cityName);
        void Delete(int cityNameId);
        int Count(int cityId);
        ApiOkPagedResponse<IEnumerable<CityName>, MetaData>
            Search(CityNameReqSearch dto);
    }
}
