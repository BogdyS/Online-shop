namespace Core.Classes.Items
{
    public class ShopItemEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string[] ImageUrls { get; set; }
    }
}
