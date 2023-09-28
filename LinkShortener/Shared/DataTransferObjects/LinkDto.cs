using System.ComponentModel.DataAnnotations;
namespace LinkShortener.Shared.DataTransferObjects;

public sealed class LinkDto
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "UrlLong is a required field.")]
    public string? UrlLong { get; set; }

    public string? UrlShort { get; set; }

    public DateTime CreationDate { get; set; }

    public int NumberOfClicks { get; set; }
}