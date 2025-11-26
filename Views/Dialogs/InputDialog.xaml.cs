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
    public partial class InputDialog : Window
    {
        public string InputText { get; set; } = string.Empty;
        public string Message { get; set; } = "";
        public string TitleText { get; set; } = "";

        public InputDialog(string title, string message)
        {
            InitializeComponent();
            Title = title;
            Message = message;
            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
