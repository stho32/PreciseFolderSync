namespace Pfs.BL.Syncing.IoHandlers;

public class IoOperationFactory : IIoOperationFactory
{
    public IIoOperation CreateDirectory(string path, string relativePath)
    {
        return new CreateDirectoryIoOperation(path, relativePath);
    }

    public IIoOperation DeleteDirectory(string path, string relativePath)
    {
        return new DeleteDirectoryIoOperation(path, relativePath);
    }

    public IIoOperation CopyFile(string fromFilePath, string toFilePath, string relativePath)
    {
        return new CopyFileIoOperation(fromFilePath, toFilePath, relativePath);
    }

    public IIoOperation RemoveFile(string filePath, string relativePath)
    {
        return new RemoveFileIoOperation(filePath, relativePath);
    }
}