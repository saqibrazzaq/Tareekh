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
        CountryName? Get(int countryNameId);
        CountryName? Create(CountryName countryName);
        CountryName? Update(int countryNameId, CountryName countryName);
        void Delete(int countryNameId);
        int Count(int countryId);
        ApiOkPagedResponse<IEnumerable<CountryName>, MetaData>
            Search(CountryNameReqSearch dto);
    }
}
