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

        // modelBuilder.Entity<Ticket>().Property(t => t.Status).HasConversion(new EnumCollectionJsonValueConverter<TicketStatus>())
        //     .Metadata.SetValueComparer(new CollectionValueComparer<TicketStatus>());
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
}