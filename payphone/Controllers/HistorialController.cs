using DTO.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace payphone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController(IHistorialService historial) : ControllerBase
    {
      
        // GET api/<HistorialController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetHistorialsDto>>> Get(int id)
        {
            var result = await historial.GetHistorialByPedidoId(id);
            return Ok(result);
        }

        // POST api/<HistorialController>
        [HttpPost]
        public async Task<ActionResult<GetHistorialsDto>> Post([FromBody] HistorialDto newHistorial)
        {
            var result = await historial.CreateHistorial(newHistorial);
            return Ok(result);
        }

        
    }
}
