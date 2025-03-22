using Microsoft.EntityFrameworkCore;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
