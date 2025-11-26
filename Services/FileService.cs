using Microsoft.Win32;
using System.IO;
using System.Text;

using NotepadApp.Interfaces;

namespace NotepadApp.Services
{
    public class FileService : IFileService
    {
        public Result TryOpen(out string? text, out string? path)
        {
            (text, path) = (null, null);

            var dialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if(dialog.ShowDialog() != true)
                return Result.Cancelled;

            try
            {
                text = File.ReadAllText(dialog.FileName, Encoding.UTF8);
                path = dialog.FileName;
                return Result.Success;
            }
            catch
            {
                return Result.Error;
            }
        }

        public Result TrySave(string text, ref string? path)
        {
            // если путь ещё не был задан — SaveAs
            if(string.IsNullOrEmpty(path))
            {
                var dialog = new SaveFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                };

                if(dialog.ShowDialog() != true)
                    return Result.Cancelled;

                path = dialog.FileName;
            }

            try
            {
                File.WriteAllText(path!, text, Encoding.UTF8);
                return Result.Success;
            }
            catch
            {
                return Result.Error;
            }
        }
    }
}
