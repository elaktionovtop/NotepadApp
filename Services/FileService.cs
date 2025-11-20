using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NotepadApp.Services
{
    class FileService : Interfaces.IFileService
    {
        public string? OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text files|*.txt|All files|*.*";

            if(dialog.ShowDialog() == true)
                return File.ReadAllText(dialog.FileName);

            return null;
        }

        public void SaveFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
