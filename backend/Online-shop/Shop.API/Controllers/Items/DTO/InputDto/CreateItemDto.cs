using System.ComponentModel.DataAnnotations;

namespace Shop.API.Controllers.Items.DTO.InputDto
{
    /// <summary>
    /// DTO for Item creation.
    /// </summary>
    public class CreateItemDto
    {
        /// <summary>
        /// Item's name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Item's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Item's price.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Images of item.
        /// </summary>
        [Required]
        public IFormFile[] Images { get; set; }

        [Required]
        public int Category { get; set; }
    }
}
