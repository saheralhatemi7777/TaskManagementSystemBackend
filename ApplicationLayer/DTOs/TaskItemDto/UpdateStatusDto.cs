using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.TaskItemDto
{
    public class UpdateStatusDto
    {
        public int TaskId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
