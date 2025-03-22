using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Dto
{
    public class GetHistorialsDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Status PreviousStatus { get; set; }
        public Status NewStatus { get; set; }
        public DateTime ChangeAt { get; set; }
    }
}
