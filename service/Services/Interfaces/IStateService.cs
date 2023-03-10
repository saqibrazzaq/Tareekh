using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace service.Services.Interfaces
{
    public interface IStateService
    {
        State? Get(int stateId);
        State? GetBySlug(string slug);
        State? Create(State state);
        State? Update(int stateId, State state);
        void Delete(int stateId);
        int Count();
        int Count(int countryId);
        ApiOkPagedResponse<IEnumerable<State>, MetaData>
            Search(StateReqSearch dto);
    }
}
