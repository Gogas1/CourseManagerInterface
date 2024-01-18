using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Interfaces
{
    public interface IDialogWindow
    {
        void Close();
        void SetDialogResult(bool result);
    }
}
