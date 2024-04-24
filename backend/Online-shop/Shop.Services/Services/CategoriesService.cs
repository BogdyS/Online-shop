using Core.Classes.Categories;
using Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Entities;
using Shop.Services.Interfaces;

namespace Shop.Services.Services
{
    [AutomaticRegistration(Lifetime = ServiceLifetime.Scoped)]
    public class CategoriesService : ICategoriesService
    {
        private readonly ShopItemsContext _context;
        private readonly IS3Service _s3Service;

        public CategoriesService(ShopItemsContext context, IS3Service s3Service)
        {
            _context = context;
            _s3Service = s3Service;
        }

        public async Task<CategoryModel> CreateCategoryAsync(CreateCategoryModel model, CancellationToken cancellationToken)
        {
            var dbModel = new Category
            {
                Name = model.Name
            };

            _context.Categories.Add(dbModel);
            await _context.SaveChangesAsync(cancellationToken);

            var key = GetFileKey(dbModel.Id);
            await _s3Service.UploadFile(model.ImageStream, key);

            dbModel.FileKey = key;
            await _context.SaveChangesAsync(cancellationToken);

            return new CategoryModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                ImageUrl = await _s3Service.GetFileUrl(dbModel.FileKey)
            };
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var preFetchedCategories = await _context.Categories
                .AsNoTracking()
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    FileKey = x.FileKey
                }).ToListAsync(cancellationToken);
            return (await Task.WhenAll(preFetchedCategories
                .Select(async x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = await _s3Service.GetFileUrl(x.FileKey)
                }))).ToList();
        }

        private string GetFileKey(int id)
        {
            return $"category_{id}_{Guid.NewGuid()}";
        }
    }
}
