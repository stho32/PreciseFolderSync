namespace Pfs.BL.Syncing.IoHandlers;

public class CopyFileIoOperation : IIoOperation
{
    public string FromPath { get; }
    public string ToPath { get; }
    public string RelativePath { get; }

    public CopyFileIoOperation(string fromPath, string toPath, string relativePath)
    {
        FromPath = fromPath;
        ToPath = toPath;
        RelativePath = relativePath;
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