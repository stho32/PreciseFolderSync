namespace Pfs.BL.Syncing.IoHandlers;

public class CreateDirectoryIoOperation : IIoOperation
{
    public string Path { get; }

    public CreateDirectoryIoOperation(string path)
    {
        Path = path;
    }

    public IoOperationResult Execute()
    {
        try
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            return new IoOperationResult(true, $"Directory exists: {Path}", this);
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error ensuring directory exists {Path}: {ex.Message}", this);
        }
    }
}