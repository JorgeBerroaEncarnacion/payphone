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
    public virtual ICollection<EstadosHistorial> EstadosHistorials { get; set; }
}

