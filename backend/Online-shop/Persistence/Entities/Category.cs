using Persistense.Abstractions.Entities;

namespace Persistence.Entities
{
    public class Category : EntityWithId
    {
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }
}
