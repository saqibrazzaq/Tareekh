using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace data.Repository
{
    public static class CityRepositoryExtensions
    {
        public static IQueryable<City> Search(this IQueryable<City> items,
            CityReqSearch searchParams)
        {
            var itemsToReturn = items
                .Include(x => x.CityNames)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.Slug.Contains(searchParams.SearchText)
                );
            }

            if (searchParams.StateId != null)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.StateId == searchParams.StateId);
            }

            return itemsToReturn;
        }
        public static IQueryable<City> Sort(this IQueryable<City> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Slug);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<City>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Slug);

            return items.OrderBy(orderQuery);
        }
    }
}
