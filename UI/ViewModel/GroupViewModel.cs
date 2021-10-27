using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class GroupViewModel
    {
        [Required]
        [Display(Name = "Наименование группы")]
        public string GroupName { get; set; }

        public UserViewModel UserModel { get; set; }
        //public string UserId { get; set; }
    }
}
