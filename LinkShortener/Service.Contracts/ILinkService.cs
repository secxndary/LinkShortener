using LinkShortener.Shared.DataTransferObjects;
namespace LinkShortener.Service.Contracts;

public interface ILinkService
{
    Task<IEnumerable<LinkDto>> GetAllLinksAsync();
    Task<LinkDto> GetLinkAsync(Guid id);
    Task<LinkDto> GetLinkByUrlShortAsync(string urlShort);
    Task<LinkDto> CreateLinkAsync(LinkDto link);
    Task<LinkDto> UpdateLinkAsync(Guid id, LinkDto link);
    Task DeleteLinkAsync(Guid id);
}
