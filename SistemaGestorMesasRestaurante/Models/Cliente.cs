using System;
using System.Collections.Generic;

namespace SistemaGestorMesasRestaurante.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
