using Persistense.Abstractions.Entities;

namespace Persistence.Entities
{
    public class Item : TimeTrackableEntityWithId
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<ItemImage> Images { get; set; }
    }
}
