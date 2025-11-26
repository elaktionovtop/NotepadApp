using NotepadApp.Views.Dialogs;

using System.Windows;

namespace NotepadApp.Services
{
    public class DialogService : IDialogService
    {
        public void ShowError(string message, string title = "Ошибка") =>
            MessageBox.Show(message, title, 
                            MessageBoxButton.OK, 
                            MessageBoxImage.Error);

        public void ShowInfo(string message, string title = "Информация") =>
            MessageBox.Show(message, title, 
                            MessageBoxButton.OK, 
                            MessageBoxImage.Information);

        public DialogResult Confirm(string message, string title = "Выбор") =>
            MessageBox.Show(message, title, 
                            MessageBoxButton.YesNoCancel, 
                            MessageBoxImage.Question)
            switch
            {
                MessageBoxResult.Yes => DialogResult.Yes,
                MessageBoxResult.No => DialogResult.No,
                _ => DialogResult.Cancel,
            };

        public string? Input(string title, string message)
        {
            var dlg = new InputDialog(title, message) { Owner = Application.Current.MainWindow };
            return dlg.ShowDialog() == true ? dlg.InputText : null;
        }

        public (string find, string? replace) InputReplace(string title, string findLabel, string replaceLabel)
        {
            var dlg = new ReplaceDialog(title, findLabel, replaceLabel) { Owner = Application.Current.MainWindow };
            if(dlg.ShowDialog() == true)
                return (dlg.FindText, dlg.ReplaceText);
            return (string.Empty, null);
        }
    }
}
