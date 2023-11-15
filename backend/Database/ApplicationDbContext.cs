using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Framework> Frameworks { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración adicional de modelos, relaciones, etc.
        // Por ejemplo, establecer claves primarias, foráneas, etc.
        modelBuilder.Entity<Profile>().HasKey(p => p.ProfileId);
        modelBuilder.Entity<Framework>().HasKey(f => f.FrameworkId);
        modelBuilder.Entity<Hobby>().HasKey(h => h.HobbyId);

        // Relaciones entre entidades
        modelBuilder.Entity<Profile>()
            .HasMany(p => p.Frameworks)
            .WithOne(f => f.Profile)
            .HasForeignKey(f => f.ProfileId);

        modelBuilder.Entity<Profile>()
            .HasMany(p => p.Hobbies)
            .WithOne(h => h.Profile)
            .HasForeignKey(h => h.ProfileId);
    }
}
