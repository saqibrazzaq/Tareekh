using dto.dtos;
using dto.Paging;
using entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public static class CityNameRepositoryExtensions
    {
        public static IQueryable<CityName> Search(this IQueryable<CityName> items,
            CityNameReqSearch searchParams)
        {
            var itemsToReturn = items
                //.Include(x => x.State.Country)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.Name.Contains(searchParams.SearchText)
                );
            }

            if (searchParams.CityId != null)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.CityId == searchParams.CityId);
            }

            return itemsToReturn;
        }
        public static IQueryable<CityName> Sort(this IQueryable<CityName> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<CityName>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
