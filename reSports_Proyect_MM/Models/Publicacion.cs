using System;
using System.Collections.Generic;

namespace reSports_Proyect_MM.Models;

public partial class Publicacion
{
    public int Id { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime FechaPublicacion { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
