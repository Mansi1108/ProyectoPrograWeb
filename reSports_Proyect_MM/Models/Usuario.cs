using System;
using System.Collections.Generic;

namespace reSports_Proyect_MM.Models;

public partial class Usuario
{
    public int Id { get; set; }

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

    public virtual ICollection<Asistencium> Asistencia { get; } = new List<Asistencium>();

    public virtual Equipo? Equipo { get; set; }

    public virtual ICollection<Publicacion> Publicacions { get; } = new List<Publicacion>();

    public virtual Rolusuario RolNavigation { get; set; } = null!;
}
