using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests
{
    public abstract class Request
    {
        public abstract void Execute(object? args);
    }
}
