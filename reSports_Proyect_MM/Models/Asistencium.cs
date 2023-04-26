using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace reSports_Proyect_MM.Models;

public partial class Asistencium
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

    public DateTime FechaAsistencia { get; set; }

    public bool Asistio { get; set; }

    public string? RazonFalta { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
