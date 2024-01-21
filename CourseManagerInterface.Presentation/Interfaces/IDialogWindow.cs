namespace CourseManagerInterface.Presentation.Interfaces
{
    public interface IDialogWindow : ICloseable
    {
        void SetDialogResult(bool result);
    }
}
