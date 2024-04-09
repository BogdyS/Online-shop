namespace Shop.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<string>> GetAutoCompleteItemNames(
            string searchPart,
            CancellationToken cancellationToken);
    }
}
