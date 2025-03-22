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

namespace Services.Services
{
    public class PedidosService(AppDBContext context, ICustomerService customer) : IPedidoService
    {
        public async Task<GetPedidoDto> CreatePedido(PedidoDTO pedido)
        {
            var myCustomer = await customer.GetCustomerById(pedido.CustomerId);
            if (myCustomer == null)
            {
                throw new ArgumentException("Customer does not exist");
            }

            ArgumentNullException.ThrowIfNull(pedido.TotalAmount);
            var newPedido = new Pedido()
            {
                CustomerId = pedido.CustomerId,
                TotalAmount = pedido.TotalAmount,
                Status = Status.Pendiente,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
           
            await context.AddAsync(newPedido);
            await context.SaveChangesAsync();
            var newPedidoDto = new GetPedidoDto()
            {
                Id =   newPedido.Id,
                CustomerId = pedido.CustomerId,
                TotalAmount = pedido.TotalAmount,
                Status = Status.Pendiente,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            return newPedidoDto;
        }

        public async Task DeletePedidoById(int id)
        {
            var myPedido = await GetPedidoById(id);
            context.Remove(myPedido);
            await context.SaveChangesAsync();
        }

        public async Task<GetPedidoDto> GetPedidoById(int id)
        {
            var myPedido = await context.FindAsync<Pedido>(id);
            if (myPedido == null)
            {
                throw new ArgumentException("Pedido does not exist");
            }
            var myPedidoDto = new GetPedidoDto()
            {
                Id = myPedido.Id,
                CustomerId = myPedido.CustomerId,
                TotalAmount = myPedido.TotalAmount,
                Status = myPedido.Status,
                CreatedAt = myPedido.CreatedAt,
                UpdatedAt = myPedido.UpdatedAt,
            };
            return myPedidoDto;
        }

        public async Task<GetPedidoDto> UpdatePedido(UpdatePedidoDto pedido)
        {
            var myPedido = await context.FindAsync<Pedido>(pedido.Id);
            if (myPedido == null)
            {
                throw new ArgumentException("Pedido does not exist");
            }
            ArgumentNullException.ThrowIfNull(pedido.TotalAmount);

            myPedido.TotalAmount = pedido.TotalAmount;
            
            myPedido.UpdatedAt = DateTime.UtcNow;
            context.Update(myPedido);
            await context.SaveChangesAsync();
            var myPedidoDto = new GetPedidoDto()
            {
                Id = myPedido.Id,
                CustomerId = myPedido.CustomerId,
                TotalAmount = myPedido.TotalAmount,
                Status = myPedido.Status,
                CreatedAt = myPedido.CreatedAt,
                UpdatedAt = myPedido.UpdatedAt,
            };
            return myPedidoDto;
        }

       
    }
}
