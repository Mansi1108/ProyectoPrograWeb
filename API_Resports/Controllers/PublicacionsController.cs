using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Resports.Models;
using reSportsModel;

namespace API_Resports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionsController : ControllerBase
    {

        // GET: api/Publicacions
        [HttpGet]
        public async Task<IEnumerable<reSportsModel.PublicacionMV>> GetPublicacions()
        {
            ResportsContext _context = new();
            IEnumerable<reSportsModel.PublicacionMV> publicacions1 = _context.Publicacions.Select(x => new reSportsModel.PublicacionMV
            {
                Id = x.Id,
                FechaPublicacion= x.FechaPublicacion,
                Mensaje = x.Mensaje,
                UsuarioId = x.UsuarioId,
            }).ToList();

            foreach (var publicacion in publicacions1)
            {
                Usuario user = _context.Usuarios.Find(publicacion.UsuarioId);
                Equipo equipo = _context.Equipos.Find(user.EquipoId);
                Rolusuario rol = _context.Rolusuarios.Find(user.Rol);
                publicacion.Usuario = new reSportsModel.UsuariosM
                {
                    NombreUsuario = user.NombreUsuario,
                    NombreCompleto = user.NombreCompleto,
                    Rol = user.Rol,
                    EquipoId = user.EquipoId,
                };
                publicacion.Equipo = new reSportsModel.EquipoM
                {
                    Nombre = equipo.Nombre,
                    Genero = equipo.Genero,
                };
                publicacion.RolNavigation = new reSportsModel.RolUsuarioM
                {
                    Nombre = rol.Nombre
                };
            }

            return publicacions1;
        }

        // GET: api/Publicacions/5
        [HttpGet("{id}")]
        public async Task<reSportsModel.PublicacionMV> GetPublicacion(int id)
        {
            ResportsContext _context = new();
            reSportsModel.PublicacionMV publicacion1 = _context.Publicacions.Select(x => new reSportsModel.PublicacionMV
            {
                Id = x.Id,
                FechaPublicacion = x.FechaPublicacion,
                Mensaje = x.Mensaje,
                UsuarioId = x.UsuarioId,
            }).FirstOrDefault(x => x.Id == id);
            Usuario user = _context.Usuarios.Find(publicacion1.UsuarioId);
            publicacion1.Usuario = new reSportsModel.UsuariosM
            {
                NombreUsuario = user.NombreUsuario,
                NombreCompleto = user.NombreCompleto,
                Rol = user.Rol,
                EquipoId = user.EquipoId,
            };
            return publicacion1;
        }

        // PUT: api/Publicacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async void PutPublicacion(int id, Publicacion publicacion)
        {
            ResportsContext _context = new();
            Publicacion publicacion1 = new Publicacion
            {
                Id = id,
                FechaPublicacion = DateTime.Now,
                Mensaje = publicacion.Mensaje,
                UsuarioId = publicacion.UsuarioId,
            };
            _context.Update(publicacion1);
            await _context.SaveChangesAsync();
        }

        // POST: api/Publicacions
        [HttpPost]
        public async Task<Publicacion> PostPublicacion(reSportsModel.PublicacionM publicacion)
        {
            ResportsContext _context = new();
            Publicacion publicacion1 = new Publicacion
            {
                FechaPublicacion = DateTime.Now,
                Mensaje = publicacion.Mensaje,
                UsuarioId = publicacion.UsuarioId,
            };
            _context.Add(publicacion1);
            await _context.SaveChangesAsync();
            return publicacion1;
        }

            // DELETE: api/Publicacions/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            ResportsContext _context = new();
            if (_context.Publicacions == null)
            {
                return NotFound();
            }
            var publicacion = await _context.Publicacions.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            _context.Publicacions.Remove(publicacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublicacionExists(int id)
        {
            ResportsContext _context = new();
            return (_context.Publicacions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
