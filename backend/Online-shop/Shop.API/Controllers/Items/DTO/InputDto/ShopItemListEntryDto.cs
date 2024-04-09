namespace Shop.API.Controllers.Items.DTO.InputDto
{
    public class ShopItemListEntryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
