using System;
using System.Collections.Generic;

namespace SistemaGestorMesasRestaurante.Models;

public partial class Horario
{
    public int HorarioId { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
