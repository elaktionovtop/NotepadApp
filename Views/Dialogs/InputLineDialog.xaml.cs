using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotepadApp.Views.Dialogs
{
    public partial class InputLineDialog : Window
    {
        public string InputText { get; set; } = string.Empty;
        public string Message { get; set; } = "";
        public string TitleText { get; set; } = "";
        public string ErrorText { get; set; } = "";

        private readonly int _maxLine;

        public InputLineDialog(string title, string message, int currentLine, int maxLine)
        {
            InitializeComponent();
            TitleText = title;

            Message = $"{message}  (текущая: {currentLine}, всего: {maxLine})";
            _maxLine = maxLine;

            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inputBox.Focus();
            inputBox.SelectAll();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(InputText, out int line) || line < 1 || line > _maxLine)
            {
                ErrorText = "Некорректный номер строки.";
                DataContext = null;     // чтобы обновилось
                DataContext = this;
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
