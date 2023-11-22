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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] Profile profile)
        {
            if (profile == null || id != profile.ProfileId)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El perfil no puede estar vacío y el ID debe coincidir.",
                    Status = 400,
                });
            }

            try
            {
                // Cargar el perfil existente desde la base de datos.
                var existingProfile = await _context.Profile
                    .Include(p => p.Frameworks)
                    .Include(p => p.Hobbies)
                    .FirstOrDefaultAsync(p => p.ProfileId == id);

                if (existingProfile == null)
                {
                    return NotFound("Perfil no encontrado.");
                }

                // Actualizar solo las propiedades principales del perfil.
                existingProfile.Name = profile.Name;
                existingProfile.Lastname = profile.Lastname;
                existingProfile.Email = profile.Email;
                existingProfile.City = profile.City;
                existingProfile.Country = profile.Country;
                existingProfile.Summary = profile.Summary;
                existingProfile.Instagram = profile.Instagram;
                existingProfile.Facebook = profile.Facebook;
                existingProfile.Age = profile.Age;

                // Guardar los cambios en la base de datos.
                await _context.SaveChangesAsync();

                return Ok(existingProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Error",
                    Detail = $"Error al actualizar el perfil: {ex.Message}",
                    Status = 500,
                });
            }
        }



            [HttpDelete("framework/{id}")]
            public async Task<IActionResult> DeleteFramework(int id)
            {
                var framework = await _context.Framework.FindAsync(id);
                if (framework == null)
                {
                    return NotFound("Framework no encontrado.");
                }

                _context.Framework.Remove(framework);
                await _context.SaveChangesAsync();

                return Ok("Framework eliminado correctamente.");
            }

            [HttpDelete("hobby/{id}")]
            public async Task<IActionResult> DeleteHobby(int id)
            {
                var hobby = await _context.Hobby.FindAsync(id);
                if (hobby == null)
                {
                    return NotFound("Hobby no encontrado.");
                }

                _context.Hobby.Remove(hobby);
                await _context.SaveChangesAsync();

                return Ok("Hobby eliminado correctamente.");
            }

            [HttpPut("hobby/{id}")]
        public async Task<IActionResult> UpdateHobby(int id, [FromBody] Hobby hobby)
        {
            if (hobby == null || id != hobby.HobbyId)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El hobby no puede estar vacío y el ID debe coincidir.",
                    Status = 400,
                });
            }

            try
            {
                // Cargar el hobby existente desde la base de datos.
                var existingHobby = await _context.Hobby.FindAsync(id);

                if (existingHobby == null)
                {
                    return NotFound("Hobby no encontrado.");
                }

                // Actualizar solo las propiedades del hobby.
                existingHobby.Name = hobby.Name;
                existingHobby.Description = hobby.Description;
                var profile = await _context.Profile.FindAsync(hobby.ProfileId);
                existingHobby.Profile = profile;

                // No es necesario actualizar ProfileId ya que se mantiene desde la base de datos.

                // Guardar los cambios en la base de datos.
                await _context.SaveChangesAsync();

                return Ok(existingHobby);
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Error",
                    Detail = $"Error al actualizar el hobby: {ex.Message}",
                    Status = 500,
                });
            }
        }

        [HttpPut("framework/{id}")]
        public async Task<IActionResult> UpdateFramework(int id, [FromBody] Framework framework)
        {
            if (framework == null || id != framework.FrameworkId)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El framework no puede estar vacío y el ID debe coincidir.",
                    Status = 400,
                });
            }

            try
            {
                // Cargar el framework existente desde la base de datos.
                var existingFramework = await _context.Framework.FindAsync(id);

                if (existingFramework == null)
                {
                    return NotFound("Framework no encontrado.");
                }

                // Actualizar solo las propiedades del framework.
                existingFramework.Name = framework.Name;
                existingFramework.Level = framework.Level;
                existingFramework.Year = framework.Year;
                var profile = await _context.Profile.FindAsync(existingFramework.ProfileId);
                existingFramework.Profile = profile;

                // No es necesario actualizar ProfileId ya que se mantiene desde la base de datos.

                // Guardar los cambios en la base de datos.
                await _context.SaveChangesAsync();

                return Ok(existingFramework);
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Error",
                    Detail = $"Error al actualizar el framework: {ex.Message}",
                    Status = 500,
                });
            }
        }



}
