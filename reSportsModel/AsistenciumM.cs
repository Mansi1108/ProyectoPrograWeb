using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reSportsModel
{
    public class AsistenciumM
    {
        public int Id { get; set; }

        public DateTime FechaAsistencia { get; set; }

        public bool Asistio { get; set; }

        public string? RazonFalta { get; set; }

        public int? UsuarioId { get; set; }
        public UsuariosM? Usuario { get; set; }
    }
}
