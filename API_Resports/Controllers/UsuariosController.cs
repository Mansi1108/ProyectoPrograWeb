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
    public class UsuariosController : ControllerBase
    {

        // GET: api/Usuarios
        [HttpGet]
        public async Task<IEnumerable<reSportsModel.UsuarioMV>> GetUsuarios()
        {
            ResportsContext _context = new();
            IEnumerable<reSportsModel.UsuarioMV> usuarios1 = _context.Usuarios.Select(x => new reSportsModel.UsuarioMV
            {
                Id = x.Id,
                NombreUsuario = x.NombreUsuario,
                Contrasena = x.Contrasena,
                Email= x.Email,
                NombreCompleto= x.NombreCompleto,
                Genero= x.Genero,
                Edad = x.Edad,
                Experiencia= x.Experiencia,
                Posicion= x.Posicion,
                Rol = x.Rol,
                EquipoId = x.EquipoId,
            }).ToList();

            foreach (var usuario in usuarios1)
            {
                Equipo equipo = _context.Equipos.Find(usuario.EquipoId);
                Rolusuario rol = _context.Rolusuarios.Find(usuario.Rol);
                usuario.RolNavigation = new reSportsModel.RolUsuarioM
                {
                    Nombre = rol.Nombre,
                };
                usuario.Equipo = new reSportsModel.EquipoM
                {
                    Nombre = equipo.Nombre
                };
            }

            return usuarios1;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<reSportsModel.UsuarioMV> GetUsuario(int id)
        {
            ResportsContext _context = new();
            reSportsModel.UsuarioMV usuario1 = _context.Usuarios.Select(x => new reSportsModel.UsuarioMV
            {
                Id = x.Id,
                NombreUsuario = x.NombreUsuario,
                Contrasena = x.Contrasena,
                NombreCompleto = x.NombreCompleto,
                Email = x.Email,
                Genero = x.Genero,
                Edad = x.Edad,
                Experiencia = x.Experiencia,
                Posicion = x.Posicion,
                Rol = x.Rol,
                EquipoId = x.EquipoId,
            }).FirstOrDefault(x => x.Id == id);

            Equipo equipo = _context.Equipos.Find(usuario1.EquipoId);
            Rolusuario rol = _context.Rolusuarios.Find(usuario1.Rol);
            usuario1.RolNavigation = new reSportsModel.RolUsuarioM
            {
                Nombre = rol.Nombre,
            };
            usuario1.Equipo = new reSportsModel.EquipoM
            {
                Nombre = equipo.Nombre
            };
            return usuario1;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async void PutUsuario(int id, reSportsModel.UsuariosM usuario)
        {
            ResportsContext _context = new();

            Usuario usuario1 = new Usuario
            {
                Id = id,
                NombreUsuario = usuario.NombreUsuario,
                Contrasena = usuario.Contrasena,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Genero = usuario.Genero,
                Edad = usuario.Edad,
                Experiencia = usuario.Experiencia,
                Posicion = usuario.Posicion,
                Rol = usuario.Rol,
                EquipoId = usuario.EquipoId,
            };
            _context.Update(usuario1);
            await _context.SaveChangesAsync();
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<Usuario> PostUsuario(reSportsModel.UsuariosM usuario)
        {
            ResportsContext _context = new();
            Usuario usuario1 = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Contrasena = usuario.Contrasena,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Genero = usuario.Genero,
                Edad = usuario.Edad,
                Experiencia = usuario.Experiencia,
                Posicion = usuario.Posicion,
                Rol = usuario.Rol,
                EquipoId = usuario.EquipoId,
            };
            _context.Add(usuario1);
            await _context.SaveChangesAsync();
            return usuario1;
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            ResportsContext _context = new();
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            ResportsContext _context = new();
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
