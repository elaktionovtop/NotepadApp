namespace NotepadApp.Interfaces
{
    public enum Result { Success, Cancelled, Error }

    public interface IFileService
    {
        Result TryOpen(out string? text, out string? path);
        Result TrySave(string text, ref string? path);
    }
}

