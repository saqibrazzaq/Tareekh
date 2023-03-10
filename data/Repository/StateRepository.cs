using data.Repository.Interfaces;
using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace data.Repository
{
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        public StateRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<State> Search(StateReqSearch dto, bool trackChanges)
        {
            var entities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges)
                .Search(dto)
                .Count();
            return new PagedList<State>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
