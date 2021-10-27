using BLL.TDO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Текст задачи")]
        public string IssueText { get; set; }

        [Required]
        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Заметка")]
        public string Note { get; set; }

        [Required]
        [Display(Name = "Повтор")]
        public int? Repetition { get; set; } = 0;

        [Required]
        [Display(Name = "Напомнить")]
        public DateTime? ReminderDate { get; set; }

        [Required]
        [Display(Name = "Добавить в \"Мой день\"")]
        public bool IncludeMyDay { get; set; } = false;

        [Required]
        [Display(Name = "Дата завершения")]
        public DateTime? CompletionDate { get; set; }

        [Required]
        [Display(Name = "Прикрепляемый файл")]
        public string AttachedFile { get; set; }

        public List<ChecklistDto> Checklists { get; set; }
        public string SelectedChecklist { get; set; }
        public int Selected { get; set; }
    }
}
