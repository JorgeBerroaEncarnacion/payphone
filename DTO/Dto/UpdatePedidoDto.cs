using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Dto;

public class UpdatePedidoDto
{
    public int Id { get; set; }
    [Range(1, int.MaxValue)]
    public int TotalAmount { get; set; } = 1;
}
