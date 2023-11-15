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
            var profile = await _context.Profiles
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
            Name = "John",
            Lastname = "Doe",
            Email = "john.doe@example.com",
            City = "Cityville",
            Country = "Countryland",
            Summary = "Desarrollador de software apasionado",
            Instagram = "Desarrollador de software apasionado",
            Facebook = "Facebook",
            YearsOld = 22,
            Frameworks = new[]
            {
                new { Name = "React", Level = "Avanzado", Year = 2016 },
                new { Name = "ASP.NET Core", Level = "Intermedio", Year = 2018 },
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
