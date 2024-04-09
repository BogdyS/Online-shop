using Core.Classes.Categories;
using Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Shop.Services.Interfaces;

namespace Shop.Services.Services
{
    [AutomaticRegistration(Lifetime = ServiceLifetime.Scoped)]
    public class CategoriesService : ICategoriesService
    {
        private readonly ShopItemsContext _context;

        public CategoriesService(ShopItemsContext context)
        {
            _context = context;
        }

        public Task<List<CategoryModel>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            return _context.Categories
                .AsNoTracking()
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(cancellationToken);
        }
    }
}
