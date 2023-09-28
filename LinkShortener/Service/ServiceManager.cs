using AutoMapper;
using LinkShortener.Contracts;
using LinkShortener.Service.Contracts;
namespace LinkShortener.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ILinkService> _linkService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper)
    {
        _linkService = new Lazy<ILinkService>(() => new LinkService(repository, mapper));
    }

    public ILinkService LinkService => _linkService.Value;
}