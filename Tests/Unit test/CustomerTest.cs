using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Services.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using DTO.Dto;
using Services.Interfaces;
namespace Tests;

public class CustomerTest
{
    private readonly Data.AppDBContext _context;
    private readonly CustomerService _customerService;

    public CustomerTest()
    {
        var options = new DbContextOptionsBuilder<Data.AppDBContext>()
             .UseInMemoryDatabase(databaseName: "payphone")
             .Options;

        _context = new Data.AppDBContext(options);
        _customerService = new CustomerService(_context);
    }

    [Fact]
    public async Task CreateCustomer_ShouldAddCustomer()
    {
        var customer = new Customer { Name = "Test Customer" };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.Customers.FirstOrDefaultAsync(c => c.Name == "Test Customer");

        Assert.NotNull(result);
        Assert.Equal("Test Customer", result.Name);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomer()
    {
        var customer = new Customer { Name = "Jorge" };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.Customers.FindAsync(customer.Id);

        Assert.NotNull(result);
        Assert.Equal("Jorge", result.Name);
    }
}
