using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using reSports_Proyect_MM.Models;
using reSports_Proyect_MM.Services;

namespace reSports_Proyect_MM.Controllers
{
    public class EquipoesController : Controller
    {
        private readonly Services.APIServices _services = new();
        public EquipoesController()
        {
            _services.SetModule("Equipoes");
        }

        // GET: Equipoes
        public async Task<IActionResult> Index()
        {
            var equipos = await _services.Get<IEnumerable<Equipo>>();
            return equipos != null ? View(equipos) : Problem("There are no movies to show");
        }

        // GET: Equipoes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var equipo = await _services.Get<Equipo>(id.ToString());

            if (equipo == null) return NotFound();

            reSportsModel.EquipoM equipo1 = new reSportsModel.EquipoM
            {
                Id = id,
                Nombre = equipo.Nombre,
                Genero = equipo.Genero
            };

            return View(equipo1);
        }

        // GET: Equipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Genero")] Equipo equipo)
        {
            await _services.Post(equipo);
            return RedirectToAction(nameof(Index));
        }

        // GET: Equipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var equipo = await _services.Get<Equipo>(id.ToString());

            if (equipo == null) return NotFound();

            return View(equipo);
        }

        // POST: Equipoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Genero")] Equipo equipo)
        {
            await _services.Put(equipo, equipo.Id.ToString());
            return RedirectToAction(nameof(Index));
        }

        // GET: Equipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var equipo = await _services.Get<Equipo>(id.ToString());

            if (equipo == null) return NotFound();

            return View(equipo);
        }

        // POST: Equipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.Delete(id.ToString());
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(int id)
        {
            ResportsContext _context = new();
            return (_context.Equipos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
