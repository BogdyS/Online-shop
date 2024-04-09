namespace Persistense.Abstractions.Entities.Interfaces
{
    public interface ITimeTrackable
    {
        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
