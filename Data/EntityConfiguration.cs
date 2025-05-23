﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        builder.ToTable("pedidos");
        builder.Property(i => i.Status).HasDefaultValue(Status.Pendiente);
        builder.HasOne(r => r.Customer)
            .WithMany(ur => ur.Pedidos)
            .HasForeignKey(ur => ur.CustomerId);

        
    }
}


public class HistorialConfiguration : IEntityTypeConfiguration<EstadosHistorial>
{
    public void Configure(EntityTypeBuilder<EstadosHistorial> builder)
    {
        builder.ToTable("estadoshistoriales");
        builder.HasOne(r => r.Pedido)
            .WithMany(ur => ur.EstadosHistorials)
            .HasForeignKey(ur => ur.OrderId);


    }
}