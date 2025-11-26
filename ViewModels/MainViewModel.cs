using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NotepadApp.Interfaces;
using NotepadApp.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

using static System.Net.Mime.MediaTypeNames;

namespace NotepadApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public DocumentModel Document { get; }

        public FileViewModel FileVM { get; }
        public EditViewModel EditVM { get; }

        public MainViewModel(DocumentModel document,
                             FileViewModel fileVm,
                             EditViewModel editVm)
        {
            (Document, FileVM, EditVM) = (document, fileVm, editVm);
            Document.PropertyChanged += Document_PropertyChanged;
        }

        private void Document_PropertyChanged
            (object? sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(Document.Text) :
                    Document.IsModified = true;
                    break;
                case nameof(Document.FilePath):
                    Document.Title = 
                        string.IsNullOrEmpty(Document.FilePath) ?
                        DocumentModel.DefaultTitle :
                        Path.GetFileName(Document.FilePath);
                    break;
                case nameof(Document.IsModified):
                    FileVM.SaveCommand.NotifyCanExecuteChanged();
                    break;
            }
        }
    }
}