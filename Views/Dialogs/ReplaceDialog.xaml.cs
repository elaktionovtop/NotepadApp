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
    public partial class ReplaceDialog : Window
    {
        public string FindText { get; set; } = "";
        public string ReplaceText { get; set; } = "";
        public string FindLabel { get; set; } = "Найти:";
        public string ReplaceLabel { get; set; } = "Заменить на:";

        public ReplaceDialog(string title, string findLabel, string replaceLabel)
        {
            InitializeComponent();
            Title = title;
            FindLabel = findLabel;
            ReplaceLabel = replaceLabel;
            DataContext = this;
        }

        public void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
