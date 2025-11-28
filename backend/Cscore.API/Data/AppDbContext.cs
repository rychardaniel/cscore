using Cscore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cscore.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ChampionshipModel> Championships { get; set; }
    public DbSet<MatchModel> Matches { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChampionshipModel>(entity =>
        {
            entity.ToTable("championship");
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(c => c.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(c => c.University)
                .HasColumnName("university")
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(c => c.StartDate)
                .HasColumnName("start_date")
                .IsRequired();

            entity.Property(c => c.EndDate)
                .HasColumnName("end_date")
                .IsRequired();

            entity.HasMany(c => c.Matches)
                .WithOne(m => m.Championship)
                .HasForeignKey(m => m.ChampionshipId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MatchModel>(entity =>
        {
            entity.ToTable("match");
            entity.HasKey(m => m.Id);

            entity.Property(m => m.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(m => m.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(m => m.TypeMatch)
                .HasColumnName("type_match")
                .IsRequired();

            entity.Property(m => m.ChampionshipId)
                .HasColumnName("championship_id")
                .IsRequired();
        });

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(150);
            
            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            entity.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();
        });
    }
}