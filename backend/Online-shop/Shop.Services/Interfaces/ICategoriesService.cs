using Core.Classes.Categories;

namespace Shop.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<CategoryModel>> GetCategoriesAsync(CancellationToken cancellationToken);
    }
}
