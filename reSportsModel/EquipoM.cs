using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reSportsModel
{
    public class EquipoM
    {
        public int Id { get; set; }

        [Required(ErrorMessage="El nombre es requerido")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El Género es requerido")]
        [DisplayName("Género")]
        public int Genero { get; set; }

        public int GeneroAux { get; set; }
    }
}
