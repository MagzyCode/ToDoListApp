using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class ChecklistViewModel
    {
        // public int Id { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string ListName { get; set; }
        public int? GroupId { get; set; }


        public UserViewModel UserModel { get; set; }
        // public Group Group { get; set; }
        //public string UserId { get; set; }

    }
}
