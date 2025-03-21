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
    public async Task<Customer> CreateCustomer(CustomerDto customer)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(customer.Name);
        var newCustomer = new Customer()
        {
            Name = customer.Name
        };
        await context.AddAsync(newCustomer);
        await context.SaveChangesAsync();
        return newCustomer;
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

       var myCustomer = await context.FindAsync<Customer>(id);
        if (myCustomer == null)
        {
            throw new ArgumentException("Customer does not exist");
        }
        return myCustomer;
        
    }

}
