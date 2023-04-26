using API_Resports.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Resports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciumsController : ControllerBase
    {

        // GET: api/Asistenciums
        [HttpGet]
        public async Task<IEnumerable<reSportsModel.AsistenciumM>> GetAsistencia()
        {
            ResportsContext _context = new();
            IEnumerable<reSportsModel.AsistenciumM> asistencias1 = _context.Asistencia.Select(x => new reSportsModel.AsistenciumM
            {
                Id = x.Id,
                FechaAsistencia = x.FechaAsistencia,
                RazonFalta = x.RazonFalta,
                Asistio = x.Asistio,
                UsuarioId = x.UsuarioId,
            }).ToList();

            foreach (var asistencia in asistencias1)
            {
                Usuario user = _context.Usuarios.Find(asistencia.UsuarioId);
                asistencia.Usuario = new reSportsModel.UsuariosM
                {
                    NombreUsuario = user.NombreUsuario
                };
            }
            return asistencias1;
        }

        // GET: api/Asistenciums/5
        [HttpGet("{id}")]
        public async Task<reSportsModel.AsistenciumM> GetAsistencium(int id)
        {
            ResportsContext _context = new();
            reSportsModel.AsistenciumM asistencia1 = _context.Asistencia.Select(x => new reSportsModel.AsistenciumM
            {
                Id = x.Id,
                FechaAsistencia = x.FechaAsistencia,
                RazonFalta = x.RazonFalta,
                Asistio = x.Asistio,
                UsuarioId = x.UsuarioId
            }).FirstOrDefault(x => x.Id == id);
            return asistencia1;
        }

        // PUT: api/Asistenciums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async void PutAsistencium(int id, reSportsModel.AsistenciumM asistencium)
        {
            ResportsContext _context = new();
            Asistencium asistencia1 = new Asistencium
            {
                Id = id,
                FechaAsistencia = asistencium.FechaAsistencia,
                RazonFalta = asistencium.RazonFalta,
                Asistio = asistencium.Asistio,
                UsuarioId = asistencium.UsuarioId
            };
            _context.Update(asistencia1);
            await _context.SaveChangesAsync();
        }

        // POST: api/Asistenciums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Asistencium> PostAsistencium(reSportsModel.AsistenciumM asistencium)
        {
            ResportsContext _context = new();
            Asistencium asistencia1 = new Asistencium
            {
                FechaAsistencia = asistencium.FechaAsistencia,
                RazonFalta = asistencium.RazonFalta,
                Asistio = asistencium.Asistio,
                UsuarioId = asistencium.UsuarioId
            };
            _context.Add(asistencia1);
            await _context.SaveChangesAsync();
            return asistencia1;
        }

        // DELETE: api/Asistenciums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencium(int id)
        {
            ResportsContext _context = new();
            if (_context.Asistencia == null)
            {
                return NotFound();
            }
            var asistencium = await _context.Asistencia.FindAsync(id);
            if (asistencium == null)
            {
                return NotFound();
            }

            _context.Asistencia.Remove(asistencium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsistenciumExists(int id)
        {
            ResportsContext _context = new();
            return (_context.Asistencia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
