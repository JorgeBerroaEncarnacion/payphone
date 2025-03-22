namespace Model.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Pedido> Pedidos { get; set; }
}

