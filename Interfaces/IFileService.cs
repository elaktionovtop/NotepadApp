using System;
using System.Collections.Generic;
using System.Text;

namespace NotepadApp.Interfaces
{
    public interface IFileService
    {
        string? OpenFile();
        void SaveFile(string path, string content);
    }

}

