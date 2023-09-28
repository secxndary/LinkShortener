using LinkShortener.Entities;
using Microsoft.EntityFrameworkCore;
namespace LinkShortener.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    { }

    public DbSet<Link>? Links { get; set; }
}