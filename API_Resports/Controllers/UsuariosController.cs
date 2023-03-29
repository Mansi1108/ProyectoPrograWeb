using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Resports.Models;

namespace API_Resports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            ResportsContext _context = new();
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<reSportsModel.UsuariosM> GetUsuario(int id)
        {
            ResportsContext _context = new();
            reSportsModel.UsuariosM usuario1 = _context.Usuarios.Select(x => new reSportsModel.UsuariosM
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
