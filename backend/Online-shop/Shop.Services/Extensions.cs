using Microsoft.EntityFrameworkCore;
using Shop.Services.Common;

namespace Shop.Services
{
    public static class Extensions
    {
        public static async Task<PaginatedResponse<T>> PaginateAsync<T>(this IQueryable<T> query, PaginationRequest? pagination)
        {
            var page = pagination.Page > 0 ? pagination.Page : 1;

            var size = pagination.Size > 0 ?
                pagination.Size : 15;

            var skip = (page - 1) * size;

            var totalCount = await query.CountAsync();

            query = query.Skip(skip)
                .Take(size);

            var result = await query.ToListAsync();

            return new PaginatedResponse<T>
            {
                Result = result,
                PaginationContext = new PaginationContext
                {
                    Page = page,
                    Size = size,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(((double)totalCount / (double)size)),
                }
            };
        }
    }
}
