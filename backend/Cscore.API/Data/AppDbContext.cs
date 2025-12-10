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
    public DbSet<MatchParticipantModel> MatchParticipants { get; set; }
    public DbSet<ChampionshipJudgeModel> ChampionshipJudges { get; set; }
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

            entity.HasMany(c => c.Judges)
                .WithOne(cj => cj.Championship)
                .HasForeignKey(cj => cj.ChampionshipId)
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

            entity.Property(m => m.SportType)
                .HasColumnName("sport_type")
                .IsRequired();

            entity.Property(m => m.Status)
                .HasColumnName("status")
                .IsRequired();

            entity.Property(m => m.ScheduledDate)
                .HasColumnName("scheduled_date")
                .IsRequired();

            entity.Property(m => m.StartedAt)
                .HasColumnName("started_at");

            entity.Property(m => m.FinishedAt)
                .HasColumnName("finished_at");

            entity.Property(m => m.Venue)
                .HasColumnName("venue")
                .HasMaxLength(200);

            entity.Property(m => m.MongoScoreId)
                .HasColumnName("mongo_score_id")
                .HasMaxLength(24); // MongoDB ObjectId length

            entity.Property(m => m.Notes)
                .HasColumnName("notes")
                .HasMaxLength(1000);

            entity.Property(m => m.ChampionshipId)
                .HasColumnName("championship_id")
                .IsRequired();

            entity.HasMany(m => m.Participants)
                .WithOne(p => p.Match)
                .HasForeignKey(p => p.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MatchParticipantModel>(entity =>
        {
            entity.ToTable("match_participant");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.MatchId)
                .HasColumnName("match_id")
                .IsRequired();

            entity.Property(p => p.Type)
                .HasColumnName("type")
                .IsRequired();

            entity.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(p => p.Side)
                .HasColumnName("side")
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.LogoUrl)
                .HasColumnName("logo_url")
                .HasMaxLength(500);

            entity.Property(p => p.Result)
                .HasColumnName("result");
        });

        modelBuilder.Entity<ChampionshipJudgeModel>(entity =>
        {
            entity.ToTable("championship_judge");
            entity.HasKey(cj => cj.Id);

            entity.Property(cj => cj.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(cj => cj.ChampionshipId)
                .HasColumnName("championship_id")
                .IsRequired();

            entity.Property(cj => cj.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(cj => cj.AssignedAt)
                .HasColumnName("assigned_at")
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

            entity.Property(u => u.Role)
                .HasColumnName("role")
                .IsRequired()
                .HasDefaultValue(RoleType.Judge);

            entity.HasMany(u => u.ChampionshipJudges)
                .WithOne(cj => cj.User)
                .HasForeignKey(cj => cj.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}