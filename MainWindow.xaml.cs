using NotepadApp.ViewModels;

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NotepadApp
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            Closing += (s, e) =>
                e.Cancel = vm.FileVM.ShouldCancel();

            text.SelectionChanged += (s, e) =>
            {
                // Обновляем VM о позиции каретки и длине выделения
                vm.EditVM.UpdateCaret(text.SelectionStart, text.SelectionLength);
            };

            // инициализация начального состояния
            text.Loaded += (s, e) =>
            {
                text.Focus();
                vm.EditVM.UpdateCaret(text.SelectionStart, text.SelectionLength);
            }; vm.FileVM.RequestFocus = () => text.Focus();
            vm.EditVM.HighlightTextRequested += (start, length) =>
            {
                text.Focus();
                text.Select(start, length);
            };
        }

        private void Delete_CanExecute
            (object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = text != null && text.SelectionLength > 0;

        private void Delete_Executed
            (object sender, ExecutedRoutedEventArgs e) =>
            text.Text = text.Text.Remove
                (text.SelectionStart, text.SelectionLength);
    }
}