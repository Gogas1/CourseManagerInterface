using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class TestCommand : Command
    {
        private readonly TestViewModel _viewModel;

        public TestCommand(TestViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(string? args)
        {
            try
            {
                TestCommandData? data = JsonSerializer.Deserialize<TestCommandData>(args ?? string.Empty);

                if(data != null)
                {
                    _viewModel.SetTime(data.DateTime);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class TestCommandData
    {
        public DateTime DateTime { get; set; }
    }
}
