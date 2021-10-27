using BLL.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class GroupListViewModel
    {
        public List<GroupDto> Groups { get; set; }
        public UserViewModel User { get; set; }
    }
}
