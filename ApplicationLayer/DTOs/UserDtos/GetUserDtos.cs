using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.UserDtos
{
    public class GetUserDtos
    {
        public int UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } =string.Empty;

        public string Role { get; set; } 

    }
}
