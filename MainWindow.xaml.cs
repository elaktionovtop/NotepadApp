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

            text.Loaded += (s, e) => text.Focus();
            vm.FileVM.RequestFocus = () => text.Focus();
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