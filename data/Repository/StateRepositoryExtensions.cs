using dto.dtos;
using dto.Paging;
using entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace data.Repository
{
    public static class StateRepositoryExtensions
    {
        public static IQueryable<State> Search(this IQueryable<State> items,
            StateReqSearch searchParams)
        {
            var itemsToReturn = items
                .Include(x => x.Country)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.Slug.Contains(searchParams.SearchText)
                );
            }

            if (searchParams.CountryId != null)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.CountryId == searchParams.CountryId);
            }

            return itemsToReturn;
        }
        public static IQueryable<State> Sort(this IQueryable<State> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Slug);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<State>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Slug);

            return items.OrderBy(orderQuery);
        }
    }
}
