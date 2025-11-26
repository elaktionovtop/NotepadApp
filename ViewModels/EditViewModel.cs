using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NotepadApp.Models;
using NotepadApp.Services;

using System;
using System.Collections.Generic;
using System.Text;

public partial class EditViewModel : ObservableObject
{
    private readonly DocumentModel _document;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private string _findText = string.Empty;

    [ObservableProperty]
    private string _replaceText = string.Empty;

    [ObservableProperty]
    private int _searchStart;


    public EditViewModel(DocumentModel document, IDialogService dialogService)
    {
        _document = document;
        _dialogService = dialogService;
    }

    public event Action<int, int>? HighlightTextRequested;

    // int startIndex, int length
    private void OnHighlightText(int start, int length) =>
        HighlightTextRequested?.Invoke(start, length);

    [RelayCommand]
    private void Find()
    {
        string? input = _dialogService.Input("Найти:", "Введите текст для поиска");
        if(!string.IsNullOrEmpty(input))
        {
            FindText = input;
            FindNext();
        }
    }

    [RelayCommand]
    private void FindNext()
    {
        if(string.IsNullOrEmpty(FindText)) return;

        int pos = _document.Text.IndexOf(FindText, SearchStart, StringComparison.CurrentCultureIgnoreCase);
        if(pos >= 0)
        {
            SearchStart = pos + FindText.Length;
            OnHighlightText(pos, FindText.Length); 
        }
        else
        {
            // если не нашли, можно начать с начала
            SearchStart = 0;
        }
    }

    [RelayCommand]
    private void FindPrevious()
    {
        if(string.IsNullOrEmpty(FindText)) return;

        int pos = _document.Text.LastIndexOf(FindText, SearchStart, StringComparison.CurrentCultureIgnoreCase);
        if(pos >= 0)
        {
            SearchStart = pos;
            OnHighlightText(pos, FindText.Length); 
        }
        else
        {
            // если не нашли, можно начать с конца
            SearchStart = _document.Text.Length;
        }
    }

    [RelayCommand]
    private void ReplaceDialog()
    {
        var (find, replace) = _dialogService.InputReplace("Заменить", "Найти:", "Заменить на:");
        if(!string.IsNullOrEmpty(find))
        {
            FindText = find;
            ReplaceText = replace ?? string.Empty;
            Replace();
        }
    }

    [RelayCommand]
    private void Replace()
    {
        if(string.IsNullOrEmpty(FindText)) return;

        int pos = _document.Text.IndexOf(FindText, SearchStart, StringComparison.CurrentCultureIgnoreCase);
        if(pos >= 0)
        {
            _document.Text = _document.Text.Remove(pos, FindText.Length)
                                             .Insert(pos, ReplaceText);
            SearchStart = pos + ReplaceText.Length;
            OnHighlightText(pos, ReplaceText.Length);
        }
    }

    [RelayCommand]
    private void GoToLineDialog()
    {
        string? input = _dialogService.Input("Перейти", "Номер строки:");
        if(int.TryParse(input, out int lineNumber) && lineNumber > 0)
        {
            GoToLine(lineNumber);
        }
    }

    [RelayCommand]
    private void GoToLine(int lineNumber)
    {
        if(lineNumber <= 0) return;

        string[] lines = _document.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        if(lineNumber > lines.Length) lineNumber = lines.Length;

        int charIndex = 0;
        for(int i = 0; i < lineNumber - 1; i++)
            charIndex += lines[i].Length + 1; // +1 для \n или \r\n

        SearchStart = charIndex; // чтобы поиск после этого работал правильно

        OnHighlightText(charIndex, lines[lineNumber - 1].Length); // выделяем всю строку
    }

    [RelayCommand]
    private void InsertDateTime()
    {
        string now = DateTime.Now.ToString("g"); // формат "dd.MM.yyyy HH:mm" или локальный
        int insertPos = SearchStart; // текущая позиция для вставки

        _document.Text = _document.Text.Insert(insertPos, now);
        _document.IsModified = true;

        SearchStart = insertPos + now.Length;

        OnHighlightText(insertPos, now.Length); // выделяем вставленное
    }
}
