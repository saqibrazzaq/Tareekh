using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace data.Repository.Interfaces
{
    public interface IStateRepository : IRepositoryBase<State>
    {
        PagedList<State> Search(StateReqSearch dto, bool trackChanges);
    }
}
