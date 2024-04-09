using Persistense.Abstractions.Entities.Interfaces;

namespace Persistense.Abstractions.Entities
{
    public abstract class TimeTrackableEntity : ITimeTrackable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
