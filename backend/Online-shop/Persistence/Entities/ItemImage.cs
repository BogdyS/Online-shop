using Persistense.Abstractions.Entities;

namespace Persistence.Entities
{
    public class ItemImage : TimeTrackableEntityWithId
    {
        public string FileKey { get; set; }

        public int ItemId { get; set; }

        public int Order { get; set; }

        public Item Item { get; set; }
    }
}
