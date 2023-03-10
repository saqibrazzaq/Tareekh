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
    public static class TimezoneRepositoryExtensions
    {
        public static IQueryable<Timezone> Search(this IQueryable<Timezone> items,
            TimezoneReqSearch searchParams)
        {
            var itemsToReturn = items
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.TzName.Contains(searchParams.SearchText) ||
                        x.CityName.Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }
        public static IQueryable<Timezone> Sort(this IQueryable<Timezone> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.TzName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Timezone>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.TzName);

            return items.OrderBy(orderQuery);
        }
    }
}
