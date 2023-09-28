namespace LinkShortener.Entities.Exceptions;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString() => $"<h1>Error {StatusCode}: {Message}</h1>";
}