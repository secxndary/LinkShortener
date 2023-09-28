using System.ComponentModel.DataAnnotations;
namespace LinkShortener.Entities;

public class Link
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "UrlLong is a required field.")]
    public string? UrlLong { get; set; }

    [Required(ErrorMessage = "UrlShort is a required field.")]
    public string? UrlShort { get; set; }

    public DateTime CreationDate { get; set; }

    public int NumberOfClicks { get; set; }
}