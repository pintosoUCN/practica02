using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Framework> Framework { get; set; }
    public DbSet<Hobby> Hobby { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>().HasData(
            new Profile
            {
                ProfileId = 1,
                Name = "Joaquin",
                Lastname = "Pinto",
                Email = "joaquin.pinto@alumnos.ucn.cl",
                City = "Antofagasta",
                Country = "Chile",
                Summary = "Un joven estudiante dedicado a su familia y amigos con sueños de crecer como persona y profesional.",
                Instagram = "https://www.instagram.com/",
                Facebook = "https://www.facebook.com/",
                Age = 22,
                ImageUrl = "https://images.mubicdn.net/images/cast_member/2552/cache-207-1524922850/image-w856.jpg?size=800x",
            }
        );

        modelBuilder.Entity<Framework>().HasData(
            new Framework
            {
                FrameworkId = 1,
                Name = "React",
                Level = "Moderado",
                Year = 2022,
                Quantity = 50,
                ProfileId = 1
            },
            new Framework
            {
                FrameworkId = 2,
                Name = "HTML",
                Level = "Alto",
                Year = 2020,
                Quantity = 80,
                ProfileId = 1
            },
            new Framework
            {
                FrameworkId = 3,
                Name = "CSS",
                Level = "Bajo",
                Year = 2022,
                Quantity = 20,
                ProfileId = 1
            }
        );

        modelBuilder.Entity<Hobby>().HasData(
            new Hobby
            {
                HobbyId = 1,
                Name = "Jugar videojuegos",
                Description = "Jugar porque fomenta la creatividad, la sociabilidad y el desarrollo cognitivo.",
                ProfileId = 1
            },
            new Hobby
            {
                HobbyId = 2,
                Name = "Cantar",
                Description = "Cantar ya que puede aliviar el estrés, mejorar el estado de ánimo y fortalecer las habilidades comunicativas.",
                ProfileId = 1
            },
            new Hobby
            {
                HobbyId = 3,
                Name = "Bailar",
                Description = "Bailar porque combina actividad física con expresión artística, promoviendo la salud y el bienestar emocional.",
                ProfileId = 1
            }
        );
    }
}
