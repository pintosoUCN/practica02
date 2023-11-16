using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var profile = await _context.Profile
                .Include(p => p.Frameworks)
                .Include(p => p.Hobbies)
                .FirstOrDefaultAsync();

            if (profile == null)
            {
                return NotFound("No se encontró ningún perfil.");
            }

            return Ok(profile);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error al obtener el perfil: {ex.Message}");

            var profileData = new
        {
            Name = "Joaquin",
            Lastname = "Pinto",
            Email = "joaquin.pinto@alumnos.ucn.cl",
            City = "Antofagasta",
            Country = "Chile",
            Summary = "Desarrollador de software apasionado",
            Instagram = "Joaquin",
            Facebook = "Joaquin",
            YearsOld = 22,
            Frameworks = new[]
            {
                new { Name = "React", Level = "Avanzado", Year = 2022 },
                new { Name = "ASP.NET Core", Level = "Intermedio", Year = 2022 },
                // Agrega más frameworks según sea necesario
            },
            Hobbies = new[]
            {
                new { Name = "Senderismo", Description = "Explorar la naturaleza" },
                new { Name = "Fotografía", Description = "Capturar momentos especiales" },
                // Agrega más hobbies según sea necesario
            }
        };

            return StatusCode(200, profileData);
        }
    }
}
