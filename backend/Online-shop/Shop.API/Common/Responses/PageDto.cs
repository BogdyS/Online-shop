using AutoMapper;
using Shop.Services.Common;

namespace Shop.API.Common.Responses
{
    public static class PageExtensions
    {
        public static PageDto<T2> MapFromPaginatedResponse<T1, T2>(PaginatedResponse<T1> response, IMapper mapper)
        {
            return new PageDto<T2>
            {
                Items = mapper.Map<List<T2>>(response.Result),
                PaginationContext = mapper.Map<PaginationContextDto>(response.PaginationContext)
            };
        }
    }

    public class PageDto<T>
    {
        /// <summary>
        /// Items collection.
        /// </summary>
        public List<T> Items { get; set; }
        /// <summary>
        /// Pagination context.
        /// </summary>
        public PaginationContextDto PaginationContext { get; set; }

    }

    public class PaginationContextDto
    {
        /// <summary>
        /// Page.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Page's size.
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Total pages count.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Total items count.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
