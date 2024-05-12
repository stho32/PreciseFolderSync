namespace Pfs.BL.Syncing.IoHandlers;

public class RemoveFileIoOperation : IIoOperation
{
    public string Path { get; }

    public RemoveFileIoOperation(string path)
    {
        Path = path;
    }

    public IoOperationResult Execute()
    {
        try
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
                return new IoOperationResult(true, $"Deleted file: {Path}", this);
            }
            return new IoOperationResult(false, $"File does not exist: {Path}", this);
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error removing file {Path}: {ex.Message}", this);
        }
    }
}