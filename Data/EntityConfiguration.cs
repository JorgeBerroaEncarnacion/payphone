using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
namespace Data;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");
        builder.Property(i => i.Status).HasDefaultValue(0);
        builder.HasOne(r => r.Customer)
            .WithMany(ur => ur.Pedidos)
            .HasForeignKey(ur => ur.CustomerId);

        
    }
}
