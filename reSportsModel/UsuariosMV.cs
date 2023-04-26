using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reSportsModel
{
    public class UsuarioMV
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de Usuario es requerido")]
        [DisplayName("Nombre Usuario")]
        public string NombreUsuario { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string NombreCompleto { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string? Experiencia { get; set; }
        public string? Posicion { get; set; }

        public int Rol { get; set; }

        public int? EquipoId { get; set; }
        public EquipoM Equipo { get; set; }
        public RolUsuarioM RolNavigation { get; set; }
    }
}
