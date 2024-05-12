namespace Pfs.BL.Syncing.IoHandlers;

public class CopyFileIoOperation : IIoOperation
{
    public string FromPath { get; }
    public string ToPath { get; }

    public CopyFileIoOperation(string fromPath, string toPath)
    {
        FromPath = fromPath;
        ToPath = toPath;
    }

    public IoOperationResult Execute()
    {
        try
        {
            File.Copy(FromPath, ToPath, true);
            return new IoOperationResult(true, $"Copied file from {FromPath} to {ToPath}", this);
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error copying file from {FromPath} to {ToPath}: {ex.Message}", this);
        }
    }
}