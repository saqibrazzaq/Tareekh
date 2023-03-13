using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace service.Services.Interfaces
{
    public interface ICityService
    {
        CityRes? Get(int cityId);
        CityRes? GetBySlug(string slug);
        CityRes? Create(CityReqEdit dto);
        CityRes? Update(int cityId, CityReqEdit dto);
        void Delete(int cityId);
        int Count();
        int Count(int stateId);
        ApiOkPagedResponse<IEnumerable<CityRes>, MetaData>
            Search(CityReqSearch dto);
    }
}
