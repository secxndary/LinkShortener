using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using LinkShortener.Contracts;
using LinkShortener.Entities;
using LinkShortener.Entities.Exceptions;
using LinkShortener.Service.Contracts;
using LinkShortener.Shared.DataTransferObjects;
namespace LinkShortener.Service;

public class LinkService : ILinkService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public LinkService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<LinkDto>> GetAllLinksAsync()
    {
        var links = await _repository.Link.GetAllLinksAsync(trackChanges: false);
        var linksDto = _mapper.Map<IEnumerable<LinkDto>>(links);
        return linksDto;
    }

    public async Task<LinkDto> GetLinkAsync(Guid id)
    {
        var link = await GetLinkAndCheckIfItExists(id, trackChanges: false);
        var linkDto = _mapper.Map<LinkDto>(link);
        return linkDto;
    }
    
    public async Task<LinkDto> GetLinkByUrlShortAsync(string urlShort)
    {
        var link = await _repository.Link.GetLinkByUrlShortAsync(urlShort, trackChanges: false);
        if (link is null)
            throw new LinkNotFoundException();
        var linkDto = _mapper.Map<LinkDto>(link);
        return linkDto;
    }

    public async Task<LinkDto> CreateLinkAsync(LinkDto link)
    {
        var linkEntity = _mapper.Map<Link>(link);
        
        linkEntity.UrlShort = GetBase64HashString(link.UrlLong!);
        linkEntity.CreationDate = DateTime.Now;

        _repository.Link.CreateLink(linkEntity);
        await _repository.SaveAsync();

        var linkToReturn = _mapper.Map<LinkDto>(linkEntity);
        return linkToReturn;
    }

    public async Task<LinkDto> UpdateLinkAsync(Guid id, LinkDto linkForUpdate)
    {
        var linkEntity = await GetLinkAndCheckIfItExists(id, trackChanges: true);

        linkForUpdate.UrlShort = GetBase64HashString(linkForUpdate.UrlLong!);
        linkForUpdate.NumberOfClicks = 
            (linkForUpdate.NumberOfClicks != 0 && linkForUpdate.NumberOfClicks != linkEntity.NumberOfClicks)
            ? linkForUpdate.NumberOfClicks
            : linkEntity.NumberOfClicks;

        if (linkForUpdate.UrlLong != linkEntity.UrlLong)
        {
            linkForUpdate.NumberOfClicks = 0;
            linkForUpdate.CreationDate = DateTime.Now;
        }

        _mapper.Map(linkForUpdate, linkEntity);
        await _repository.SaveAsync();

        var linkToReturn = _mapper.Map<LinkDto>(linkEntity);
        return linkToReturn;
    }

    public async Task DeleteLinkAsync(Guid id)
    {
        var link = await GetLinkAndCheckIfItExists(id, trackChanges: false);
        _repository.Link.DeleteLink(link);
        await _repository.SaveAsync();
    }


    private async Task<Link> GetLinkAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var link = await _repository.Link.GetLinkAsync(id, trackChanges);
        if (link is null)
            throw new LinkNotFoundException();
        return link;
    }

    private static string GetBase64HashString(string urlLong)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(urlLong));
        var base64Hash = Convert.ToBase64String(hashBytes)
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "")
            [..10];
        return base64Hash;
    }
}