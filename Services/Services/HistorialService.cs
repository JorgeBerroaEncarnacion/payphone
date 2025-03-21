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
    public class HistorialService(AppDBContext context, IPedidoService pedido) : IHistorialService
    {
        public async Task<EstadosHistorial> CreateHistorial(HistorialDto historial)
        {
            var myPedido = await pedido.GetPedidoById(historial.OrderId);
            if (myPedido == null)
            {
                throw new ArgumentException("Pedido does not exist");
            }
            var prevStatus = myPedido.Status;
            if (IsNextStatusValid(myPedido.Status, historial.NewStatus))
            {
                myPedido.Status = historial.NewStatus;
            }
            else
            {
                throw new ArgumentException("Invalid status transition.");
            }

            var newHistorial = new EstadosHistorial()
            {
                OrderId = historial.OrderId,
                PreviousStatus = prevStatus,
                NewStatus = historial.NewStatus,
                ChangeAt = DateTime.Now
            };

            await context.AddAsync(newHistorial);
             context.Update(myPedido);
            await context.SaveChangesAsync();
            return newHistorial;

        }

        public async Task<List<EstadosHistorial>> GetHistorialByPedidoId(int id)
        {
            var myPedido = await context.FindAsync<Pedido>(id);
            if (myPedido == null)
            {
                throw new ArgumentException("Pedido does not exist");
            }
            return myPedido.EstadosHistorials.ToList();
        }

        public static bool IsNextStatusValid(Status current, Status newStatus)
        {
            if (newStatus == Status.Cancelado || current == newStatus)
                return true;

            return (int)newStatus == (int)current + 1;
        }
    }
}
