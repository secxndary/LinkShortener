using LinkShortener.Entities;
namespace LinkShortener.Contracts;

public interface ILinkRepository
{
    Task<IEnumerable<Link>> GetAllLinksAsync(bool trackChanges);
    Task<Link?> GetLinkAsync(Guid id, bool trackChanges);
    Task<Link?> GetLinkByUrlShortAsync(string urlShort, bool trackChanges);
    void CreateLink(Link link);
    void DeleteLink(Link link);
}