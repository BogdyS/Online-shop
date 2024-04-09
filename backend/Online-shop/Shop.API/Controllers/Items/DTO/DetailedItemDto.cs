namespace Shop.API.Controllers.Items.DTO
{
    public class DetailedItemDto
    {
        /// <summary>
        /// Item's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item's name.
        /// </summary>
        public string Name { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// Item's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Images.
        /// </summary>
        public string[] ImageUrls { get; set; }
    }
}
