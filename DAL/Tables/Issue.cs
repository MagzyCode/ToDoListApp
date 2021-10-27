using DAL.Tables.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Tables
{
    /// <summary>
    /// Класс, представляющий собой задачу.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Текст задачи.
        /// </summary>
        public string IssueText { get; set; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Заметка к записи.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Количество дней, через которое повторяется задача после её выполнения.
        /// </summary>
        public int? Repetition { get; set; } = 0;

        /// <summary>
        /// Дата напоминания о необходимости выполнить задачу.
        /// </summary>
        public DateTime? ReminderDate { get; set; }

        /// <summary>
        /// Значение, отражающее является ли задача выполненной
        /// </summary>
        public bool IncludeMyDay { get; set; } = false;

        /// <summary>
        /// День, в который задач должна быть выполнена.
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Название прикрепляемого файла.
        /// В теории, файлы храняться отдельно, а здесь храниться путь к ним.
        /// </summary>
        public string AttachedFile { get; set; }


        /// <summary>
        /// Идентификатор списка задач.
        /// </summary>
        
        public int? ChecklistId { get; set; }
        [ForeignKey("ChecklistId")]
        public Checklist Checklist { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
