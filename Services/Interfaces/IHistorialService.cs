using DTO.Dto;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IHistorialService
    {
        Task<EstadosHistorial> CreateHistorial(HistorialDto historial);
        Task<List<EstadosHistorial>> GetHistorialByPedidoId(int id);
    }
}
