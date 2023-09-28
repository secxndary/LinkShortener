using AutoMapper;
using LinkShortener.Entities;
using LinkShortener.Shared.DataTransferObjects;
namespace LinkShortener;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Link, LinkDto>().ReverseMap();
    }
}