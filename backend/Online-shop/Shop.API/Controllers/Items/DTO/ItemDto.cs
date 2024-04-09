namespace Shop.API.Controllers.Items.DTO
{
    public class ItemDto
    {
        /// <summary>
        /// Item's identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Item's price.
        /// </summary>
        public decimal Price { get; set; }
    }
}
