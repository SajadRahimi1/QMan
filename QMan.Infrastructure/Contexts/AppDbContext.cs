using Microsoft.EntityFrameworkCore;
using QMan.Domain.Entities.Ticket;
using QMan.Domain.Entities.User;
using QMan.Infrastructure.Helpers;

namespace QMan.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Ticket>().HasKey(ticket => ticket.Id);
        modelBuilder.Entity<TicketMessage>().HasKey(ticketMessage => ticketMessage.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);

        modelBuilder.Entity<Ticket>().HasMany(t => t.Messages).WithOne(tm => tm.Ticket)
            .HasForeignKey(tm => tm.TicketId);
    }

    public override int SaveChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            entry.Property("UpdatedAt").CurrentValue = now;

            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = now;
            }
        }
        return base.SaveChanges();
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
}