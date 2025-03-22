using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests;

public class PedidoTests
{
    private readonly Data.AppDBContext _context;
    private readonly PedidosService _pedidoService;
    private readonly CustomerService _customerService;
    public PedidoTests()
    {
        var options = new DbContextOptionsBuilder<Data.AppDBContext>()
             .UseInMemoryDatabase(databaseName: "payphone")
             .Options;

        _context = new Data.AppDBContext(options);
        _customerService = new CustomerService(_context);
        _pedidoService = new PedidosService(_context,_customerService);
    }

    [Fact]
    public async Task CreatePedido_ShouldAddPedido()
    {
        var customer = new Customer { Name = "Carlos" };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var pedido = new Pedido
        {
            CustomerId = customer.Id,
            TotalAmount = 1000,
            Status = Status.Pendiente
        };

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        var result = await _context.Pedidos.FindAsync(pedido.Id);
        Assert.NotNull(result);
        Assert.Equal(1000, result.TotalAmount);
        Assert.Equal(Status.Pendiente, result.Status);
    }

    [Fact]
    public async Task UpdatePedido_ShouldModifyStatus()
    {
        var pedido = new Pedido
        {
            CustomerId = 1,
            TotalAmount = 500,
            Status = Status.Pendiente
        };
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        pedido.Status = Status.Procesado;
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();

        var updated = await _context.Pedidos.FindAsync(pedido.Id);
        Assert.Equal(Status.Procesado, updated.Status);
    }

    [Fact]
    public async Task DeletePedido_ShouldRemovePedido()
    {
        var pedido = new Pedido { CustomerId = 1, TotalAmount = 800, Status = Status.Pendiente };
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();

        var deleted = await _context.Pedidos.FindAsync(pedido.Id);
        Assert.Null(deleted);
    }

    
}
