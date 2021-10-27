using BLL.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class PlannedTaskListViewModel
    {
        public List<IssueDto> Issues { get; set; }
        public UserViewModel User { get; set; }
    }
}
