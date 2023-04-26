using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reSportsModel
{
    public class PublicacionM
    {
        public int Id { get; set; }

        public string Mensaje { get; set; } = null!;

        public DateTime FechaPublicacion { get; set; }

        public int? UsuarioId { get; set; }
    }
}
