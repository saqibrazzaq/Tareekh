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
    public interface ICountryNameService
    {
        CountryNameRes? Get(int countryNameId);
        CountryNameRes? Create(CountryNameReqEdit dto);
        CountryNameRes? Update(int countryNameId, CountryNameReqEdit dto);
        void Delete(int countryNameId);
        int Count(int countryId);
        ApiOkPagedResponse<IEnumerable<CountryNameRes>, MetaData>
            Search(CountryNameReqSearch dto);
    }
}
