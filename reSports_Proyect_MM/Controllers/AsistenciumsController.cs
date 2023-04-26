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

namespace reSports_Proyect_MM.Controllers
{
    public class AsistenciumsController : Controller
    {
        private readonly Services.APIServices _services = new();
        public AsistenciumsController()
        {
            _services.SetModule("Asistenciums");
        }

        // GET: Asistenciums
        public async Task<IActionResult> Index()
        {
            var asistencias = await _services.Get<IEnumerable<reSports_Proyect_MM.Models.Asistencium>>();
            return asistencias != null ? View(asistencias) : Problem("There are no asistencias to show");
        }

        // GET: Asistenciums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencium = await _context.Asistencia
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asistencium == null)
            {
                return NotFound();
            }

            return View(asistencium);
        }

        // GET: Asistenciums/Create
        public IActionResult Create()
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario");
            return View();
        }

        // POST: Asistenciums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaAsistencia,Asistio,RazonFalta,UsuarioId")] reSports_Proyect_MM.Models.Asistencium asistencium)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            if(asistencium.RazonFalta == "")
            {
                asistencium.RazonFalta = "N/A";
            }
            await _services.Post(asistencium);
            
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", asistencium.UsuarioId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Asistenciums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            var asistencium = await _services.Get<Models.Asistencium>(id.ToString());

            if (asistencium == null) return NotFound();
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", asistencium.UsuarioId);

            return View(asistencium);
        }

        // POST: Asistenciums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaAsistencia,Asistio,RazonFalta,UsuarioId")] reSports_Proyect_MM.Models.Asistencium asistencium)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            if(asistencium.Asistio == true)
            {
                asistencium.RazonFalta = "N/A";
            }
            await _services.Put<Models.Asistencium>(asistencium, id.ToString());
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", asistencium.UsuarioId);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Asistenciums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var asistencia = await _services.Get<reSports_Proyect_MM.Models.Asistencium>(id.ToString());

            if (asistencia == null) return NotFound();

            return View(asistencia);
        }

        // POST: Asistenciums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.Delete(id.ToString());
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciumExists(int id)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            return (_context.Asistencia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
