using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Resports.Models;
using System.Diagnostics.Metrics;

namespace API_Resports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoesController : ControllerBase
    {

        // GET: api/Equipoes
        [HttpGet]
        public async Task<IEnumerable<reSportsModel.EquipoM>> GetEquipos()
        {
            ResportsContext _context = new();

            IEnumerable<reSportsModel.EquipoM> equipos1 = _context.Equipos.Select(x => new reSportsModel.EquipoM
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Genero = x.Genero,
            }).ToList();

            return equipos1;
        }

        // GET: api/Equipoes/5
        [HttpGet("{id}")]
        public async Task<reSportsModel.EquipoM> GetEquipo(int id)
        {
            ResportsContext _context = new();
            reSportsModel.EquipoM equipo1 = _context.Equipos.Select(x => new reSportsModel.EquipoM
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Genero = x.Genero
            }).FirstOrDefault(x => x.Id == id);

            return equipo1;
        }

        // PUT: api/Equipoes/5
        [HttpPut("{id}")]
        public async void PutEquipo(int id, reSportsModel.EquipoM equipo)
        {
            ResportsContext _context = new();

            Equipo equipo1 = new Equipo
            {
                Id = id,
                Nombre = equipo.Nombre,
                Genero = equipo.Genero
            };
            _context.Update(equipo1);
            await _context.SaveChangesAsync();
        }

        // POST: api/Equipoes
        [HttpPost]
        public async Task<Equipo> PostEquipo(reSportsModel.EquipoM equipo)
        {
            ResportsContext _context = new();
            Equipo equipo1 = new Equipo
            {
                Nombre = equipo.Nombre,
                Genero = equipo.Genero
            };
            _context.Add(equipo1);
            await _context.SaveChangesAsync();
            return equipo1;
        }

        // DELETE: api/Equipoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipo(int id)
        {
            ResportsContext _context = new();
            if (_context.Equipos == null)
            {
                return NotFound();
            }
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipoExists(int id)
        {
            ResportsContext _context = new();
            return (_context.Equipos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
