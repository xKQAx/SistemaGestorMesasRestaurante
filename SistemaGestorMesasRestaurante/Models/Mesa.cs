using System;
using System.Collections.Generic;

namespace SistemaGestorMesasRestaurante.Models;

public partial class Mesa
{
    public int MesaId { get; set; }

    public int NumeroMesa { get; set; }

    public short Capacidad { get; set; }

    public string? Ubicacion { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
