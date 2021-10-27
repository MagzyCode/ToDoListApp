using DAL.Tables.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Tables
{
    public class Checklist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string ListName { get; set; }
            public int? GroupId { get; set; }

            [ForeignKey("GroupId")]
            public Group Group { get; set; }
            public string UserId { get; set; }

            [ForeignKey("UserId")]
            public User User { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
