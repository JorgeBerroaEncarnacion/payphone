using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Dto
{
    public class HistorialDto
    {
        public int OrderId { get; set; }
        public Status NewStatus { get; set; }
    }
}
