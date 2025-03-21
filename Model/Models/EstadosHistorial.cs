namespace Model.Models;

public class EstadosHistorial
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Status PreviousStatus { get; set; }
    public Status NewStatus { get; set; }

    public DateTime ChangeAt { get; set; } = DateTime.Now;

    public virtual Pedido Pedido { get; set; }
}

