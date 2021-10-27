using BLL.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class GroupChecklistsListViewModel
    {
        public List<ChecklistDto> Checklists { get; set; }
        public string GroupName { get; set; }
        public UserViewModel User { get; set; }
    }
}
