using System;
using System.Collections.Generic;

namespace SistemaGestorMesasRestaurante.Models;

public partial class Notificacion
{
    public int NotificacionId { get; set; }

    public int ClienteId { get; set; }

    public int ReservaId { get; set; }

    public string Tipo { get; set; } = null!;

    public DateTime FechaEnvio { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Reserva Reserva { get; set; } = null!;
}
