using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Business;
using QMan.Domain.Entities.Category;
using QMan.Domain.Entities.Comment;
using QMan.Domain.Entities.ContactUs;
using QMan.Domain.Entities.Otp;
using QMan.Domain.Entities.Plans;
using QMan.Domain.Entities.Product;
using QMan.Domain.Entities.Theme;
using QMan.Domain.Entities.Ticket;

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
        modelBuilder.Entity<Admin>().HasKey(u => u.Id);
        modelBuilder.Entity<Comment>().HasKey(u => u.Id);
        modelBuilder.Entity<Address>().HasKey(u => u.Id);
        modelBuilder.Entity<Business>().HasKey(u => u.Id);
        modelBuilder.Entity<Category>().HasKey(u => u.Id);
        modelBuilder.Entity<SubCategory>().HasKey(u => u.Id);
        modelBuilder.Entity<ContactUs>().HasKey(u => u.Id);
        modelBuilder.Entity<Product>().HasKey(u => u.Id);
        modelBuilder.Entity<Otp>().HasKey(u => u.Id);
        modelBuilder.Entity<Access>().HasKey(u => u.Id);
        modelBuilder.Entity<Theme>().HasKey(u => u.Id);
        modelBuilder.Entity<ThemeColor>().HasKey(u => u.Id);
        modelBuilder.Entity<Plan>().HasKey(u => u.Id);

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
        modelBuilder.Entity<Product>().HasOne(p => p.SubCategory).WithMany(c => c.Products)
            .HasForeignKey(p => p.SubcategoryId);
        modelBuilder.Entity<Product>().HasOne(p => p.Business).WithMany(c => c.Products)
            .HasForeignKey(p => p.BusinessId);
        modelBuilder.Entity<BaseProduct>().HasOne(p => p.SubCategory).WithMany(c => c.BaseProducts)
            .HasForeignKey(p => p.SubcategoryId);
        modelBuilder.Entity<BaseProduct>().HasMany(p => p.Businesses).WithMany(b => b.BaseProducts);
        modelBuilder.Entity<Access>().HasMany(p => p.Admins).WithMany(b => b.Access);
        modelBuilder.Entity<Theme>().HasMany(t => t.ThemeColors).WithOne(tc => tc.Theme)
            .HasForeignKey(tc => tc.ThemeId);
        modelBuilder.Entity<Business>().HasOne(b => b.ThemeColor).WithMany(tc => tc.BusinessesSelected)
            .HasForeignKey(b => b.SelectedThemeColorId);
    }

    public override int SaveChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            entry.Property("UpdateDateTime").CurrentValue = now;

            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedDateTime").CurrentValue = now;
            }
        }

        return base.SaveChanges();
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<BaseProduct> BaseProducts { get; set; }
    public DbSet<Otp> Otps { get; set; }
    public DbSet<Access> Accesses { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Theme> Themes { get; set; }
    public DbSet<ThemeColor> ThemeColors { get; set; }
}