using Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Shop.Services.Interfaces;

namespace Shop.Services.Services
{
    [AutomaticRegistration(Lifetime = ServiceLifetime.Scoped)]
    public class SearchService : ISearchService
    {
        private readonly ShopItemsContext _context;

        public SearchService(ShopItemsContext context)
        {
            _context = context;
        }

        public Task<List<string>> GetAutoCompleteItemNames(string searchPart, CancellationToken cancellationToken)
        {
            return _context.Items
                .Where(x => EF.Functions.Like(x.Name.ToLower(), "%" + searchPart.ToLower() + "%"))
                .Select(x => x.Name)
                .Distinct()
                .OrderBy(x => x.ToLower().IndexOf(searchPart.ToLower()))
                .ToListAsync(cancellationToken);
        }
    }
}
