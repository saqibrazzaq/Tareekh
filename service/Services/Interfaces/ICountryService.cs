using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace service.Services.Interfaces
{
    public interface ICountryService
    {
        CountryRes? Get(int countryId);
        CountryRes? GetBySlug(string slug);
        CountryRes? Create(CountryReqEdit dto);
        CountryRes? Update(int countryId, CountryReqEdit dto);
        void Delete(int countryId);
        int Count();
        ApiOkPagedResponse<IEnumerable<CountryRes>, MetaData>
            Search(CountryReqSearch dto);
    }
}
