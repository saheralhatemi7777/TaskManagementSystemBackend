using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.BaseValidationDtos
{
    public class BaseValidtionDto
    {
        public bool success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
