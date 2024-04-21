using AutoMapper;
using Core.Classes.Items;
using Core.DI;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Entities;
using Shop.Services.Common;
using Shop.Services.Interfaces;
using Shop.Services.Models;

namespace Shop.Services.Services
{
    [AutomaticRegistration(Lifetime = ServiceLifetime.Scoped)]
    public class ItemsService : IItemsService
    {
        private readonly ShopItemsContext _context;
        private readonly IMapper _mapper;
        private readonly IS3Service _s3Service;

        public ItemsService(
            ShopItemsContext context,
            IMapper mapper,
            IS3Service s3Service)
        {
            _context = context;
            _mapper = mapper;
            _s3Service = s3Service;
        }

        public async Task<ShopItemEntry> CreateItemAsync(CreateShopItem item, CancellationToken cancellationToken)
        {
            var itemToAdd = new Item
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.Category
            };

            _context.Items.Add(itemToAdd);

            await _context.SaveChangesAsync(cancellationToken);

            var fileKeys = new List<string>();

            foreach (var image in item.ImageStreams)
            {
                var key = GetFileKey(itemToAdd.Id);
                fileKeys.Add(key);
                await _s3Service.UploadFile(image, key);
            }

            int order = 1;
            var imagesToAdd = fileKeys
                .Select(x => new ItemImage
                {
                    FileKey = x,
                    Order = order++
                }).ToList();

            itemToAdd.Images = imagesToAdd;

            await _context.SaveChangesAsync();

            var itemResponse = _mapper.Map<ShopItemEntry>(itemToAdd);

            var getUrlsTasks = itemToAdd.Images
                .Select(x => _s3Service.GetFileUrl(x.FileKey));

            await Task.WhenAll(getUrlsTasks);

            itemResponse.ImageUrls = getUrlsTasks.Select(x => x.Result).ToArray();

            return itemResponse;
        }

        public async Task DeleteItemByIdAsync(int itemId, CancellationToken cancellationToken)
        {
            var itemToDelete = await _context.Items
                .AnyAsync(x => x.Id == itemId, cancellationToken);

            if (!itemToDelete)
            {
                throw GetItemNotFoundException();
            }

            await _context.Items
                .Where(x => x.Id == itemId)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<ShopItemEntry> GetItemDetailedAsync(int itemId, CancellationToken cancellationToken)
        {
            var item = await _context.Items
                .AsNoTracking()
                .Select(x => new ShopItemEntry
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ImageUrls = x.Images.OrderBy(x => x.Order).Select(i => i.FileKey).ToArray()
                })
                .SingleOrDefaultAsync(x => x.Id == itemId, cancellationToken);

            if (item == null)
            {
                throw GetItemNotFoundException();
            }

            item.ImageUrls = item.ImageUrls
                .Select(async x => await _s3Service.GetFileUrl(x))
                .Select(x => x.Result)
                .ToArray();

            return item;
        }

        public async Task<PaginatedResponse<ShopItemListEntry>> GetItemsAsync(
            GetItemsRequestModel request,
            CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request is not defined");
            }

            var query = _context.Items
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var searchInLovercase = request.Search.ToLower().Trim();

                query = query.Where(x => 
                x.Name.ToLower().Contains(searchInLovercase)
                || x.Description.ToLower().Contains(searchInLovercase));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= request.MinPrice);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= request.MaxPrice);
            }

            if (request.CategoriesIds is { Count: > 0 })
            {
                query = query.Where(x => request.CategoriesIds.Contains(x.CategoryId));
            }

            var itemsResponse = await query.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageKey = x.Images.OrderBy(i => i.Order).Select(i => i.FileKey).FirstOrDefault()
            }).PaginateAsync(request.Pagination);

            return new PaginatedResponse<ShopItemListEntry>
            {
                PaginationContext = itemsResponse.PaginationContext,
                Result = itemsResponse.Result
                .Select(async x => new ShopItemListEntry
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = (!string.IsNullOrEmpty(x.ImageKey) ? await _s3Service.GetFileUrl(x.ImageKey) : null)
                })
                .Select(x => x.Result)
                .ToList()
            };
        }

        public async Task<ShopItemEntry> UpdateItemByIdAsync(int itemId, ShopItem item, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _context.Items
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Id == itemId, cancellationToken);

            if (itemToUpdate == null)
            {
                throw GetItemNotFoundException();
            }

            if (!string.IsNullOrEmpty(item.Name) && !item.Name.Equals(itemToUpdate.Name))
            {
                itemToUpdate.Name = item.Name;
            }

            if (!string.IsNullOrEmpty(item.Description) && !item.Description.Equals(itemToUpdate.Description))
            {
                itemToUpdate.Description = item.Description;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ShopItemEntry>(itemToUpdate);
        }

        private AppException GetItemNotFoundException() => new AppException("Item not found");

        private string GetFileKey(int id)
        {
            return $"item_{id}_{Guid.NewGuid()}";
        }
    }
}
