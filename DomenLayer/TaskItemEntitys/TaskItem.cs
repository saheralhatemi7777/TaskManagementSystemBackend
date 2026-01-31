using DomenLayer.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DomenLayer.TaskItemEntitys
{
    public class TaskItem: BaseEntity
    {
        [Required(ErrorMessage ="حقل بيانات مطلوب")]
        [MaxLength(500,ErrorMessage ="طول السلسه غير مسموح")]
        public string Title { get; set; }

        [MaxLength(1000,ErrorMessage ="طول سلسة غير مسموح ")]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime UpdatedAt { get; set; }

        //Fk
        public int UserId { get; set; }

        public Users users { get; set; }
    }
}
