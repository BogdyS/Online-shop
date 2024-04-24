using Persistense.Abstractions.Entities;

namespace Persistence.Entities
{
    public class Category : EntityWithId
    {
        public string Name { get; set; }
        public string FileKey { get; set; }
        public List<Item> Items { get; set; }
    }
}
