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
        Task<GetHistorialsDto> CreateHistorial(HistorialDto historial);
        Task<List<GetHistorialsDto>> GetHistorialByPedidoId(int id);
    }
}
