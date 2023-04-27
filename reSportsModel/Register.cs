using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reSportsModel
{
    public class Register
    {
        public string NombreUsuario { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string NombreCompleto { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public int Rol { get; set; }
    }
}
