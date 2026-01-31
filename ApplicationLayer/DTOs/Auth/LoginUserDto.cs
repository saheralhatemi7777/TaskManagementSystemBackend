using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.Auth
{
    public class LoginUserDto
    {
        #region Login_Properties
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }= string.Empty;
        #endregion
    }
}
