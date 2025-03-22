using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Dto
{
    public class GetPedidoDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Range(1, int.MaxValue)]
        public int TotalAmount { get; set; } = 1;

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
