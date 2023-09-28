namespace LinkShortener.Contracts;

public interface IRepositoryManager
{
    ILinkRepository Link { get; }
    Task SaveAsync();
}