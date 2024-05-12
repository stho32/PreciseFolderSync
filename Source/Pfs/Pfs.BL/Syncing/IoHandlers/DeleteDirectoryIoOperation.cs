namespace Pfs.BL.Syncing.IoHandlers;

public class DeleteDirectoryIoOperation : IIoOperation
{
    public string Path { get; }
    public string RelativePath { get; }

    public DeleteDirectoryIoOperation(string path, string relativePath)
    {
        Path = path;
        RelativePath = relativePath;
    }

    public IoOperationResult Execute()
    {
        try
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
                return new IoOperationResult(true, $"Deleted directory: {Path}", this);
            }
            return new IoOperationResult(false, $"Directory does not exist: {Path}", this);
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error deleting directory {Path}: {ex.Message}", this);
        }
    }
}