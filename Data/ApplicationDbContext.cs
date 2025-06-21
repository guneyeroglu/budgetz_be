using Microsoft.EntityFrameworkCore;
using BudgetZ.API.Models;

namespace BudgetZ.API.Data;

// Veritabanı bağlantısı ve varlık yapılandırmalarını yöneten sınıf
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Veritabanı tabloları
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Expense> Expenses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Kullanıcı varlık yapılandırması
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            
            entity.HasIndex(u => u.Username).IsUnique();
            
            entity.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");
            
            entity.Property(u => u.Username)
                .HasColumnName("username")
                .HasMaxLength(20)
                .IsRequired();
            
            entity.Property(u => u.Password)
                .HasColumnName("password")
                .HasMaxLength(16)
                .IsRequired();
            
            entity.Property(u => u.IsAdmin)
                .HasColumnName("is_admin")
                .HasDefaultValue(false);
            
            entity.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");
            
            entity.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);
        });

        // Kategori varlık yapılandırması
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            
            entity.Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("integer")
                .UseIdentityColumn();
            
            entity.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();
        });

        // Harcama varlık yapılandırması
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("expenses");
            
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");
            
            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();
            
            entity.Property(e => e.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();
            
            entity.Property(e => e.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            
            entity.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired(false);
            
            entity.Property(e => e.ExpenseDate)
                .HasColumnName("expense_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();
            
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");
            
            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is User || e.Entity is Expense)
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is User || entry.Entity is Expense)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
} 