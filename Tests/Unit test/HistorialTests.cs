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

public class HistorialTests
{
    private readonly Data.AppDBContext _context;
    private readonly PedidosService _pedidoService;
    private readonly CustomerService _customerService;
    private readonly HistorialService _historialService;
    public HistorialTests()
    {
        var options = new DbContextOptionsBuilder<Data.AppDBContext>()
             .UseInMemoryDatabase(databaseName: "payphone")
             .Options;

        _context = new Data.AppDBContext(options);
        _customerService = new CustomerService(_context);
        _pedidoService = new PedidosService(_context, _customerService);
        _historialService = new HistorialService(_context,_pedidoService);
    }

    [Fact]
    public async Task CreateEstadoHistorial_ShouldSaveHistory()
    {
        var pedido = new Pedido { CustomerId = 1, TotalAmount = 900, Status = Status.Pendiente };
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        var history = new EstadosHistorial
        {
            OrderId = pedido.Id,
            PreviousStatus = Status.Pendiente,
            NewStatus = Status.Procesado,
            ChangeAt = DateTime.Now
        };

        _context.EstadosHistoriales.Add(history);
        await _context.SaveChangesAsync();

        var result = await _context.EstadosHistoriales.FirstOrDefaultAsync(h => h.OrderId == pedido.Id);
        Assert.NotNull(result);
        Assert.Equal(Status.Procesado, result.NewStatus);
    }

    [Fact]
    public async Task GetHistorialByPedidoId_ShouldReturnHistory()
    {
        var pedido = new Pedido { CustomerId = 1, TotalAmount = 1000, Status = Status.Pendiente };
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        var history = new EstadosHistorial
        {
            OrderId = pedido.Id,
            PreviousStatus = Status.Pendiente,
            NewStatus = Status.Procesado,
            ChangeAt = DateTime.Now
        };
        _context.EstadosHistoriales.Add(history);
        await _context.SaveChangesAsync();

        var list = await _context.EstadosHistoriales
            .Where(h => h.OrderId == pedido.Id)
            .ToListAsync();

        Assert.Single(list);
        Assert.Equal(Status.Procesado, list[0].NewStatus);
    }
}
