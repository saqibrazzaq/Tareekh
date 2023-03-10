using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace data.Repository.Interfaces
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        PagedList<City> Search(CityReqSearch dto, bool trackChanges);
    }
}
