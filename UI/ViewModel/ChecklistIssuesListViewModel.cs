using BLL.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class ChecklistIssuesListViewModel
    {
        public List<IssueDto> Issues { get; set; }
        //public UserViewModel User { get; set; }
        public string ChecklistName { get; set; }
    }
}
