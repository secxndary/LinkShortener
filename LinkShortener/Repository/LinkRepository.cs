using LinkShortener.Contracts;
using LinkShortener.Entities;
using Microsoft.EntityFrameworkCore;
namespace LinkShortener.Repository;

public class LinkRepository : RepositoryBase<Link>, ILinkRepository
{
    public LinkRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    { }

    public async Task<IEnumerable<Link>> GetAllLinksAsync(bool trackChanges) =>
        await FindAll(trackChanges).ToListAsync();

    public async Task<Link?> GetLinkAsync(Guid id, bool trackChanges) =>
        await FindByCondition(l => l.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public async Task<Link?> GetLinkByUrlShortAsync(string urlShort, bool trackChanges) =>
        await FindByCondition(l => l.UrlShort!.Equals(urlShort), trackChanges).SingleOrDefaultAsync();

    public void CreateLink(Link link) =>
        Create(link);

    public void DeleteLink(Link link) =>
        Delete(link);
}