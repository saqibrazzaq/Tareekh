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
        CityNameRes? Get(int cityNameId);
        CityNameRes? Create(CityNameReqEdit dto);
        CityNameRes? Update(int cityNameId, CityNameReqEdit dto);
        void Delete(int cityNameId);
        int Count(int cityId);
        ApiOkPagedResponse<IEnumerable<CityNameRes>, MetaData>
            Search(CityNameReqSearch dto);
    }
}
