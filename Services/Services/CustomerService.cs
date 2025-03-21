using Castle.Core.Resource;
using Data;
using DTO.Dto;
using Model.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services;

public class CustomerService(AppDBContext context) : ICustomerService
{
    public async Task<GetCustomerDto> CreateCustomer(CustomerDto customer)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(customer.Name);
        var newCustomer = new Customer()
        {
            Name = customer.Name
        };
        
        await context.AddAsync(newCustomer);
        await context.SaveChangesAsync();
        var newCustomerDto = new GetCustomerDto()
        {
            Id = newCustomer.Id,
            Name = customer.Name
        };
        return newCustomerDto;
    }

    public async Task<GetCustomerDto> GetCustomerById(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

       var myCustomer = await context.FindAsync<Customer>(id);
        if (myCustomer == null)
        {
            throw new ArgumentException("Customer does not exist");
        }
        var myCustomerDto = new GetCustomerDto()
        {
            Id = myCustomer.Id,
            Name = myCustomer.Name
        };
        return myCustomerDto;
        
    }

}
