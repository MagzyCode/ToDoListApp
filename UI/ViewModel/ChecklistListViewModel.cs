using BLL.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class ChecklistListViewModel
    {
        public List<ChecklistDto> Checklists { get; set; }
        public UserViewModel User { get; set; }
    }
}
