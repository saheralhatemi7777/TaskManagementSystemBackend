using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomenLayer
{
    public class BaseEntity
    {

        [Key]
        public int Pk_Id { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
