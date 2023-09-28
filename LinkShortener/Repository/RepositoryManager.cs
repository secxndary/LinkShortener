using LinkShortener.Contracts;
namespace LinkShortener.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ILinkRepository> _linkRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _linkRepository = new Lazy<ILinkRepository>(() => new LinkRepository(repositoryContext));
    }

    public ILinkRepository Link => _linkRepository.Value;
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}