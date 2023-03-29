using System;
using System.Collections.Generic;

namespace API_Resports.Models;

public partial class Rolusuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
