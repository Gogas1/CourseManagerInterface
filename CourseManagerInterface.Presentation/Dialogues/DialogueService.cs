using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManagerInterface.Presentation.Dialogues
{
    public class DialogueService
    {
        private readonly Func<Type, Window> _dialogueWindowFactory;

        public DialogueService(Func<Type, Window> dialogueWindowFactory)
        {
            _dialogueWindowFactory = dialogueWindowFactory;
        }


        public bool ShowDialog<TDialogueWindow>(ViewModel viewModel) where TDialogueWindow : Window
        {
            var targetWindow = _dialogueWindowFactory.Invoke(typeof(TDialogueWindow));
            targetWindow.DataContext = viewModel;

            var result = targetWindow.ShowDialog() ?? false;

            return result;
        }
    }
}
