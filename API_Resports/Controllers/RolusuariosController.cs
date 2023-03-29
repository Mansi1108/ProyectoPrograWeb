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
    public class RolusuariosController : ControllerBase
    {

        // GET: api/Rolusuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rolusuario>>> GetRolusuarios()
        {
            ResportsContext _context = new();
            if (_context.Rolusuarios == null)
            {
              return NotFound();
            }
            return await _context.Rolusuarios.ToListAsync();
        }

        // GET: api/Rolusuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rolusuario>> GetRolusuario(int id)
        {
            ResportsContext _context = new();
            if (_context.Rolusuarios == null)
          {
              return NotFound();
          }
            var rolusuario = await _context.Rolusuarios.FindAsync(id);

            if (rolusuario == null)
            {
                return NotFound();
            }

            return rolusuario;
        }

        // PUT: api/Rolusuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolusuario(int id, Rolusuario rolusuario)
        {
            ResportsContext _context = new();
            if (id != rolusuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(rolusuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolusuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rolusuarios
        [HttpPost]
        public async Task<ActionResult<Rolusuario>> PostRolusuario(Rolusuario rolusuario)
        {
            ResportsContext _context = new();
            if (_context.Rolusuarios == null)
          {
              return Problem("Entity set 'ResportsContext.Rolusuarios'  is null.");
          }
            _context.Rolusuarios.Add(rolusuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRolusuario", new { id = rolusuario.Id }, rolusuario);
        }

        // DELETE: api/Rolusuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolusuario(int id)
        {
            ResportsContext _context = new();
            if (_context.Rolusuarios == null)
            {
                return NotFound();
            }
            var rolusuario = await _context.Rolusuarios.FindAsync(id);
            if (rolusuario == null)
            {
                return NotFound();
            }

            _context.Rolusuarios.Remove(rolusuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolusuarioExists(int id)
        {
            ResportsContext _context = new();
            return (_context.Rolusuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
