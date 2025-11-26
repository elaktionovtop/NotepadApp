using System;
using System.Collections.Generic;
using System.Text;

using CommunityToolkit.Mvvm.ComponentModel;

namespace NotepadApp.Models
{
    public partial class DocumentModel : ObservableObject
    {
        public const string DefaultTitle = "Без имени";

        [ObservableProperty]
        private string _text = string.Empty;

        [ObservableProperty]
        private string? _filePath;

        [ObservableProperty]
        private bool _isModified;

        [ObservableProperty]
        private string _title = DefaultTitle;
    }
}
