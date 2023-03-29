using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Resports.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using reSports_Proyect_MM.Models;
using reSportsModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace reSports_Proyect_MM.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Services.APIServices _services = new();
        public UsuariosController()
        {
            _services.SetModule("Usuarios");
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            API_Resports.Models.ResportsContext _context = new();
            var resportsContext = _context.Usuarios.Include(u => u.Equipo).Include(u => u.RolNavigation);
            var usuarios = await _services.Get<IEnumerable<reSports_Proyect_MM.Models.Usuario>>();

            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            API_Resports.Models.ResportsContext _context = new();
            var usuario = await _services.Get<reSports_Proyect_MM.Models.Usuario>(id.ToString());

            if (usuario == null) return NotFound();

            reSportsModel.UsuariosM equipo1 = new reSportsModel.UsuariosM
            {
                Id = id,
            };

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            API_Resports.Models.ResportsContext _context = new();
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "Id", "Nombre");
            ViewData["Rol"] = new SelectList(_context.Rolusuarios, "Id", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreUsuario,Contrasena,Email,NombreCompleto,Genero,Edad,Experiencia,Posicion,Rol,EquipoId")] reSports_Proyect_MM.Models.Usuario usuario)
        {
            await _services.Post(usuario);
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            var usuario = await _services.Get<reSports_Proyect_MM.Models.Usuario>(id.ToString());
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "Id", "Nombre", usuario.EquipoId);
            ViewData["Rol"] = new SelectList(_context.Rolusuarios, "Id", "Nombre", usuario.Rol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreUsuario,Contrasena,Email,NombreCompleto,Genero,Edad,Experiencia,Posicion,Rol,EquipoId")] reSports_Proyect_MM.Models.Usuario usuario)
        {
            API_Resports.Models.ResportsContext _context = new();

            
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "Id", "Id", usuario.EquipoId);
            ViewData["Rol"] = new SelectList(_context.Rolusuarios, "Id", "Id", usuario.Rol);

            await _services.Put(usuario, usuario.Id.ToString());
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var usuario = await _services.Get<reSports_Proyect_MM.Models.Usuario>(id.ToString());

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.Delete(id.ToString());
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            API_Resports.Models.ResportsContext _context = new();
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
