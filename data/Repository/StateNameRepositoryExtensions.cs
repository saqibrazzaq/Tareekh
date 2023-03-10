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
    public static class StateNameRepositoryExtensions
    {
        public static IQueryable<StateName> Search(this IQueryable<StateName> items,
            StateNameReqSearch searchParams)
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

            if (searchParams.StateId != null)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => x.StateId == searchParams.StateId);
            }

            return itemsToReturn;
        }
        public static IQueryable<StateName> Sort(this IQueryable<StateName> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<StateName>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
