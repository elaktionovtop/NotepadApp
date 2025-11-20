using System;
using System.Collections.Generic;
using System.Text;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NotepadApp.Interfaces;

using static System.Net.Mime.MediaTypeNames;

namespace NotepadApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IFileService _fileService;

        [ObservableProperty]
        private string text;

        public MainViewModel(IFileService fileService)
        {
            _fileService = fileService;
        }

        [RelayCommand]
        private void Open()
        {
            var content = _fileService.OpenFile();
            if(content != null)
                Text = content;
        }

        [RelayCommand]
        private void Save()
        {
            // для простоты — фиксированный путь
            _fileService.SaveFile("saved.txt", Text ?? "");
        }
    }
}
