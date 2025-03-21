using Data;
using DTO.Dto;
using Microsoft.EntityFrameworkCore;
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
        public async Task<GetHistorialsDto> CreateHistorial(HistorialDto historial)
        {
            var myPedido = await context.Pedidos.FindAsync(historial.OrderId);
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
            var newHistorialDto = new GetHistorialsDto()
            {
                Id = newHistorial.OrderId,
                OrderId = historial.OrderId,
                PreviousStatus = prevStatus,
                NewStatus = historial.NewStatus,
                ChangeAt = DateTime.Now
            };
            return newHistorialDto;

        }

        public async Task<List<GetHistorialsDto>> GetHistorialByPedidoId(int id)
        {
            var exists = await context.Pedidos.AnyAsync(p => p.Id == id);
            if (!exists)
                throw new ArgumentException("Pedido does not exist");

            return await context.EstadosHistoriales
                .Where(h => h.OrderId == id)
                .OrderBy(h => h.ChangeAt)
                .Select(h => new GetHistorialsDto
                {
                    Id = h.Id,
                    OrderId = h.OrderId,
                    PreviousStatus = h.PreviousStatus,
                    NewStatus = h.NewStatus,
                    ChangeAt = h.ChangeAt
                })
                .ToListAsync();
        }

        public static bool IsNextStatusValid(Status current, Status newStatus)
        {
            if (newStatus == Status.Cancelado || current == newStatus)
                return true;

            return (int)newStatus == (int)current + 1;
        }
    }
}
