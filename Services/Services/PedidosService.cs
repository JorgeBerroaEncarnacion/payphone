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
        public async Task<Pedido> CreatePedido(PedidoDTO pedido)
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
                Status = Status.Pendiente
            };
            await context.AddAsync(newPedido);
            await context.SaveChangesAsync();
            return newPedido;
        }

        public async Task DeletePedidoById(int id)
        {
            var myPedido = await GetPedidoById(id);
            context.Remove(myPedido);
            await context.SaveChangesAsync();
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            var myPedido = await context.FindAsync<Pedido>(id);
            if (myPedido == null)
            {
                throw new ArgumentException("Pedido does not exist");
            }
            return myPedido;
        }

        public async Task<Pedido> UpdatePedido(UpdatePedidoDto pedido)
        {
            var myPedido = await GetPedidoById(pedido.Id);
           
            ArgumentNullException.ThrowIfNull(pedido.TotalAmount);

            myPedido.TotalAmount = pedido.TotalAmount;
            if (IsNextStatusValid(myPedido.Status, pedido.Status))
            {
                myPedido.Status = pedido.Status;
            }
            else
            {
                throw new ArgumentException("Invalid status transition.");
            }
            myPedido.UpdatedAt = DateTime.UtcNow;
            context.Update(myPedido);
            await context.SaveChangesAsync();
            return myPedido;
        }

        public static bool IsNextStatusValid(Status current, Status newStatus)
        {
            if (newStatus == Status.Cancelado || current == newStatus)
                return true;

            return (int)newStatus == (int)current + 1;
        }
    }
}
