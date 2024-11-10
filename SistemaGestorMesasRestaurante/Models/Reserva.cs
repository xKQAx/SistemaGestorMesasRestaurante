using System;
using System.Collections.Generic;

namespace SistemaGestorMesasRestaurante.Models;

public partial class Reserva
{
    public int ReservaId { get; set; }

    public int ClienteId { get; set; }

    public int MesaId { get; set; }

    public int HorarioId { get; set; }

    public DateOnly FechaReserva { get; set; }

    public string? Estado { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Horario Horario { get; set; } = null!;

    public virtual Mesa Mesa { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();
}
