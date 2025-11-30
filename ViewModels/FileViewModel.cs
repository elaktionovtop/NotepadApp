using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NotepadApp.Interfaces;
using NotepadApp.Models;
using NotepadApp.Services;

using System.Windows.Input;

namespace NotepadApp.ViewModels
{
    public partial class FileViewModel : ObservableObject
    {
        private readonly DocumentModel _doc;
        
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;

        public FileViewModel(DocumentModel document,
            IFileService fileService, IDialogService dialogService) =>
            (_doc, _fileService, _dialogService) = (document, fileService, dialogService);
        
        public Action? RequestFocus { get; set; }

        //[ObservableProperty]
        //private string _text = string.Empty;
        
        //[ObservableProperty]
        //private string? _filePath;

        //[ObservableProperty]
        //private bool _isModified;

        //[ObservableProperty]
        //private string _title = DefaultTitle;

        //partial void OnTextChanged(string? oldValue, string newValue) =>
        //    IsModified = true;

        //// Обновление заголовка окна при изменении пути файла
        //partial void OnFilePathChanged(string? oldValue, string? newValue) =>
        //    WindowTitle = newValue ?? _defaultTitle;

        //// Обновление доступности команды Сохранить
        //partial void OnIsModifiedChanged(bool value) =>
        //    SaveCommand.NotifyCanExecuteChanged();

        [RelayCommand]
        public void New()
        {
            if(ShouldCancel()) return;

            _doc.Text = string.Empty;
            CompleteUpdate();
        }

        [RelayCommand]
        public void Open()
        {
            if(ShouldCancel()) return;

            var result = _fileService
                .TryOpen(out string? content, out string? path);
            if(result == Result.Success && content != null)
            {
                _doc.Text = content;
                CompleteUpdate(path);
            }
            else if(result == Result.Error)
            {
                _dialogService.ShowError("Ошибка при чтении файла.");
            }
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        public void Save()
        {
            if(_doc.FilePath == null)
            {
                SaveAs();
                return;
            }

            string? path = _doc.FilePath;
            var result = _fileService.TrySave(_doc.Text, ref path);
            if(result == Result.Success)
            {
                CompleteUpdate(path);
            }
            else if(result == Result.Error)
            {
                _dialogService.ShowError("Ошибка при сохранении файла.");
            }
        }

        private bool CanSave() => _doc.IsModified;   

        [RelayCommand]
        public void SaveAs()
        {
            string? path = null;
            var result = _fileService.TrySave(_doc.Text, ref path);
            if(result == Result.Success && path != null)
            {
                CompleteUpdate(path);
            }
            else if(result == Result.Error)
            {
                _dialogService.ShowError("Ошибка при сохранении файла.");
            }
        }

        [RelayCommand]
        public void Exit()
        {
            if(ShouldCancel()) return;

            App.Current.Shutdown();
        }

        public bool ShouldCancel()
        {
            if(!_doc.IsModified) return false;

            var result = _dialogService
                .Confirm("Файл изменён. Сохранить изменения?");
            if(result == DialogResult.Cancel)
            {
                return true; // отмена операции
            }

            if(result == DialogResult.Yes)
            {
                Save();
            }

            return false; // пользователь выбрал No
        }

        private void CompleteUpdate(string? path = null)
        {
            _doc.FilePath = path;
            _doc.IsModified = false;
            RequestFocus?.Invoke();
        }
    }
}
