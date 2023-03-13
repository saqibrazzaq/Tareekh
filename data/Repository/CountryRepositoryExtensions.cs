using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace data.Repository
{
    public static class CountryRepositoryExtensions
    {
        public static IQueryable<Country> Search(this IQueryable<Country> items,
            CountryReqSearch searchParams)
        {
            var itemsToReturn = items
                .Include(x => x.CountryNames)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.Slug.Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }
        public static IQueryable<Country> Sort(this IQueryable<Country> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Slug);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Country>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Slug);

            return items.OrderBy(orderQuery);
        }
    }
}
