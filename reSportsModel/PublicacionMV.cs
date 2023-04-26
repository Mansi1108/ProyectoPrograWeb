using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reSportsModel
{
    public class PublicacionMV
    {
        public int Id { get; set; }

        public string Mensaje { get; set; } = null!;

        public DateTime FechaPublicacion { get; set; }

        public int? UsuarioId { get; set; }

        public UsuariosM Usuario { get; set; }
        public EquipoM Equipo { get; set; }
        public RolUsuarioM RolNavigation { get; set; }

    }
}
