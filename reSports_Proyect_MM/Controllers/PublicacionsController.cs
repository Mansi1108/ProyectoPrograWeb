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
    public class PublicacionsController : Controller
    {
        private readonly Services.APIServices _services = new();

        public PublicacionsController()
        {
            _services.SetModule("Publicacions");
        }

        // GET: Publicacions
        public async Task<IActionResult> Index()
        {
            var publicacions1 = await _services.Get<IEnumerable<reSportsModel.PublicacionMV>>();
            return View(publicacions1);
        }

        // GET: Publicacions/Create
        public IActionResult Create()
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario");
            return View();
        }

        // POST: Publicacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mensaje,FechaPublicacion,UsuarioId")] Models.Publicacion publicacion)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            await _services.Post(publicacion);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", publicacion.UsuarioId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Publicacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            var publicacion1 = await _services.Get<Models.Publicacion>(id.ToString());

            if (publicacion1 == null) return NotFound();
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", publicacion1.UsuarioId);
            return View(publicacion1);
        }

        // POST: Publicacions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mensaje,FechaPublicacion,UsuarioId")] Models.Publicacion publicacion)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            if (id != publicacion.Id)
            {
                return NotFound();
            }
            await _services.Put<Models.Publicacion>(publicacion, id.ToString());
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", publicacion.UsuarioId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Publicacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var publicacion = await _services.Get<reSports_Proyect_MM.Models.Publicacion>(id.ToString());

            if (publicacion == null) return NotFound();

            return View(publicacion);
        }

        // POST: Publicacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.Delete(id.ToString());
            return RedirectToAction(nameof(Index));
        }

        private bool PublicacionExists(int id)
        {
            reSports_Proyect_MM.Models.ResportsContext _context = new();
            return (_context.Publicacions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
