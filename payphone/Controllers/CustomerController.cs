using DTO.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace payphone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customer) : ControllerBase
    {

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> Get(int id)
        {
            var result = await customer.GetCustomerById(id);
            return Ok(result);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<ActionResult<GetCustomerDto>> Post([FromBody] CustomerDto newCustomer)
        {
           
         var result =  await customer.CreateCustomer(newCustomer);
            return Ok(result);
        }
      
    }
}
