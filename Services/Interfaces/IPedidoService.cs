using DTO.Dto;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPedidoService
    {
        Task<GetPedidoDto> CreatePedido(PedidoDTO pedido);
        Task<GetPedidoDto> GetPedidoById(int id);
        Task DeletePedidoById(int id);
        Task<GetPedidoDto> UpdatePedido(UpdatePedidoDto pedido);
    }
}
