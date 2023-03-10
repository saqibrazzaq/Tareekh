using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace service.Services.Interfaces
{
    public interface ICountryService
    {
        Country? Get(int countryId);
        Country? GetBySlug(string slug);
        Country? Create(Country country);
        Country? Update(int countryId, Country country);
        void Delete(int countryId);
        int Count();
        ApiOkPagedResponse<IEnumerable<Country>, MetaData>
            Search(CountryReqSearch dto);
    }
}
