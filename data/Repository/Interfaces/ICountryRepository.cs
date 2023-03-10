using dto.dtos;
using dto.Paging;
using entity.Entities;

namespace data.Repository.Interfaces
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        PagedList<Country> Search(CountryReqSearch dto, bool trackChanges);
    }
}
