namespace NotepadApp.Services
{
    public enum DialogResult { Yes, No, Cancel }

    public interface IDialogService
    {
        void ShowError(string message, string title = "Ошибка");
        void ShowInfo(string message, string title = "Информация");
        DialogResult Confirm(string message, string title = "Выбор");
        string? Input(string title, string message);
        string? InputLine(string title, string message, int currentLine, int maxLine);
        (string find, string? replace) InputReplace(string title, string findLabel, string replaceLabel);
    }
}
