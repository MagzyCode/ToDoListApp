using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Уникальный идентификатор")]
        public string Id { get; set; }

        ////public string Name { get; set; }
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        public DateTime DayOfBorn { get; set; }
    }
}
