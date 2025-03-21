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
        Task<Pedido> CreatePedido(PedidoDTO pedido);
        Task<Pedido> GetPedidoById(int id);
        Task DeletePedidoById(int id);
        Task<Pedido> UpdatePedido(UpdatePedidoDto pedido);
    }
}
