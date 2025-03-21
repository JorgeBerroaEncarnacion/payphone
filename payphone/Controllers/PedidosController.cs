using DTO.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace payphone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController(IPedidoService pedido) : ControllerBase
    {

        // GET api/<PedidosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> Get(int id)
        {
            var result = await pedido.GetPedidoById(id);

            return Ok(result);
        }

        // POST api/<PedidosController>
        [HttpPost]
        public async Task<ActionResult<Pedido>> Post([FromBody] PedidoDTO newPedido)
        {
            var result = await pedido.CreatePedido(newPedido);
            return Ok(result);
        }

        // PUT api/<PedidosController>/5
        [HttpPut]
        public async Task<ActionResult<Pedido>> Put([FromBody] UpdatePedidoDto updatePedido)
        {
            var result = await pedido.UpdatePedido(updatePedido);
            return Ok(result);
        }

        // DELETE api/<PedidosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await pedido.DeletePedidoById(id);
            return Ok();
        }
    }
}
