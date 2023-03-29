using System;
using System.Collections.Generic;

namespace API_Resports.Models;

public partial class Equipo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Genero { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
