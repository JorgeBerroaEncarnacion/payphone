using DTO.Dto;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces;

public interface ICustomerService
{
    Task<GetCustomerDto> CreateCustomer(CustomerDto customer);
    Task<GetCustomerDto> GetCustomerById(int id);
}
