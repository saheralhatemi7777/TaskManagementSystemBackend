using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.Auth
{
    public class ChengedPasswordDto
    {

        public string Email { get; set; }

        public string NewPassword { get; set; }= string.Empty;
    }
}
