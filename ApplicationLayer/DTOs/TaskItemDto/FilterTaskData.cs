using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.TaskItemDto
{
    public record FilterTaskData(string? Title,string? Status,string? Descreption,DateTime? CreateAt,DateTime? UpdateDate);
    
}
