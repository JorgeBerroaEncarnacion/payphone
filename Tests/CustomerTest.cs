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
    public async Task CreateCustomer_ShouldAddCustomerToDatabase()
    {
        // Arrange
        var customerDto = new CustomerDto { Name = "Test Customer" };

        // Act
        var newCustomer = await _customerService.CreateCustomer(customerDto);

        // Assert
        var result = await _customerService.GetCustomerById(newCustomer.Id);
        Assert.NotNull(result);
        Assert.Equal("Test Customer", result.Name);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnCorrectCustomer()
    {
        // Arrange
        var customerDto = new CustomerDto { Name = "Test Customer" };

        // Act
        var newCustomer = await _customerService.CreateCustomer(customerDto);

        // Assert
        var result = await _customerService.GetCustomerById(newCustomer.Id);
        Assert.NotNull(result);
        Assert.Equal("Test Customer", result.Name);
    }
}
