namespace LinkShortener.Service.Contracts;

public interface IServiceManager
{
    ILinkService LinkService { get; }
}