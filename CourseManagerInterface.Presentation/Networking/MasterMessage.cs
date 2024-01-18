using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Networking
{
    public class MasterMessage
    {
        public string Command { get; set; } = string.Empty;
        public string CommandData { get; set; } = string.Empty;
    }
}
