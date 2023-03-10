using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace service.Services.Interfaces
{
    public interface ICityService
    {
        City? Get(int cityId);
        City? GetBySlug(string slug);
        City? Create(City city);
        City? Update(int cityId, City city);
        void Delete(int cityId);
        int Count();
        int Count(int stateId);
        ApiOkPagedResponse<IEnumerable<City>, MetaData>
            Search(CityReqSearch dto);
    }
}
