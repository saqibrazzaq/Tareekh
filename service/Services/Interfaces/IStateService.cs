using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace service.Services.Interfaces
{
    public interface IStateService
    {
        StateRes? Get(int stateId);
        StateRes? GetBySlug(string slug);
        StateRes? Create(StateReqEdit dto);
        StateRes? Update(int stateId, StateReqEdit dto);
        void Delete(int stateId);
        int Count();
        int Count(int countryId);
        ApiOkPagedResponse<IEnumerable<StateRes>, MetaData>
            Search(StateReqSearch dto);
    }
}
