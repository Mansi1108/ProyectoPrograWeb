using System;
using System.Collections.Generic;

namespace API_Resports.Models;

public partial class LogReporte
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public bool Fallo { get; set; }

    public string? Descripccion { get; set; }
}
