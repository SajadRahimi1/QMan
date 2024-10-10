using Microsoft.EntityFrameworkCore;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Business;
using QMan.Domain.Entities.Category;
using QMan.Domain.Entities.Comment;
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
        modelBuilder.Entity<Admin>().HasKey(u => u.Id);
        modelBuilder.Entity<Comment>().HasKey(u => u.Id);
        modelBuilder.Entity<Address>().HasKey(u => u.Id);
        modelBuilder.Entity<Business>().HasKey(u => u.Id);
        modelBuilder.Entity<Category>().HasKey(u => u.Id);
        modelBuilder.Entity<SubCategory>().HasKey(u => u.Id);

        modelBuilder.Entity<Ticket>().HasMany(t => t.Messages).WithOne(tm => tm.Ticket)
            .HasForeignKey(tm => tm.TicketId);

        modelBuilder.Entity<Category>().HasMany(c => c.SubCategories).WithOne(sc => sc.Category)
            .HasForeignKey(sc => sc.CategoryId);

        modelBuilder.Entity<Business>().HasOne<Address>(b => b.Address).WithOne(a => a.Business)
            .HasForeignKey<Address>(a => a.BusinessId);
        modelBuilder.Entity<Business>().HasMany<Comment>(b => b.Comments).WithOne(c => c.Business)
            .HasForeignKey(c => c.BusinessId);
        modelBuilder.Entity<Business>().HasMany<Ticket>(b => b.Tickets).WithOne(t => t.Business)
            .HasForeignKey(t => t.BusinessId);
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
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
}