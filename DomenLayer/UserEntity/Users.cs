using DomenLayer.TaskItemEntitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomenLayer.UserEntity
{
    public class Users: BaseEntity
    {
        [Required(ErrorMessage ="حقل بيانات مطلوب")]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [EmailAddress(ErrorMessage ="صيغة إيميل خطاء ")]
        [MaxLength(300)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public EnumRole Role { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
