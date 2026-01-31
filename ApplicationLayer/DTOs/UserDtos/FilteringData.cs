using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.UserDtos
{
    public record FilteringData(string? UserName,string? Email,DateTime?CreatedAt);
   
}
