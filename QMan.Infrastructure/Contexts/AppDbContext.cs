using Microsoft.EntityFrameworkCore;
using QMan.Domain.Entities.AboutUs;

namespace QMan.Infrastructure.Contexts;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AboutUs> AboutUs { get; set; }
}