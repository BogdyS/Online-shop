using Persistense.Abstractions.Entities.Interfaces;

namespace Persistense.Abstractions.Entities
{
    public abstract class EntityWithId : IEntityWithId
    {
        public int Id { get; set; }
    }
}
