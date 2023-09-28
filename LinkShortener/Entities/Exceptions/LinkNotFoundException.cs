namespace LinkShortener.Entities.Exceptions;

public class LinkNotFoundException : NotFoundException
{
    public LinkNotFoundException()
        : base("The link with this URL was not found.")
    { }
}
