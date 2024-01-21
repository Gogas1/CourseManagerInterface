using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class IncomesFoundCommand : Command
    {
        private readonly IncomeManagementViewModel _incomeManagementViewModel;
        public IncomesFoundCommand(IncomeManagementViewModel incomeManagementViewModel)
        {
            _incomeManagementViewModel = incomeManagementViewModel;
        }

        public override void Execute(string? args)
        {
            CommandData data = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();

            List<Income> incomesFound = new();
            foreach (var incomeFound in data.IncomesFound)
            {
                Income income = new Income()
                {
                    Id = incomeFound.Id,
                    CreatedAt = incomeFound.CreatedAt,
                    Summ = incomeFound.Summ,
                    Supplier = incomeFound.Supplier
                };

                incomesFound.Add(income);
            }

            _incomeManagementViewModel.ShowSearchResult(incomesFound);
        }

        private class CommandData
        {
            public List<IncomeRecord> IncomesFound { get; set; } = new();
        }

        private class IncomeRecord
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Supplier { get; set; } = string.Empty;
            public decimal Summ { get; set; }
        }
    }
}
