using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.Models;

public class Pedido
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [Range(1, int.MaxValue)]
    public int TotalAmount { get; set; } = 1;

    public Status Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public virtual Customer Customer { get; set; }
}

public enum Status
{
    Pendiente = 0,
    Procesado = 1,
    Enviado = 2,
    Entregado = 3,
    Cancelado = 4
}

public class EstadosHistorial
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Status PreviousStatus { get; set; }
    public Status NewStatus { get; set; }

    public DateTime ChangeAt { get; set; } = DateTime.Now;
}


public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Pedido> Pedidos { get; set; }
}

