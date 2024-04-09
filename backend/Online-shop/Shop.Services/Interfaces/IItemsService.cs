using Core.Classes.Items;
using Shop.Services.Common;
using Shop.Services.Models;

namespace Shop.Services.Interfaces
{
    public interface IItemsService
    {
        Task<PaginatedResponse<ShopItemListEntry>> GetItemsAsync(GetItemsRequestModel request, CancellationToken cancellationToken = default);
        Task<ShopItemEntry> GetItemDetailedAsync(int itemId, CancellationToken cancellationToken);
        Task<ShopItemEntry> CreateItemAsync(CreateShopItem item, CancellationToken cancellationToken);
        Task<ShopItemEntry> UpdateItemByIdAsync(int itemId, ShopItem item, CancellationToken cancellationToken);
        Task DeleteItemByIdAsync(int itemId, CancellationToken cancellationToken);
    }
}
