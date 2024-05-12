namespace Pfs.BL.Syncing.IoHandlers;

public class IoOperationFactory : IIoOperationFactory
{
    public IIoOperation CreateDirectory(string path)
    {
        return new CreateDirectoryIoOperation(path);
    }

    public IIoOperation DeleteDirectory(string path)
    {
        return new DeleteDirectoryIoOperation(path);
    }

    public IIoOperation CopyFile(string fromFilePath, string toFilePath)
    {
        return new CopyFileIoOperation(fromFilePath, toFilePath);
    }

    public IIoOperation RemoveFile(string filePath)
    {
        return new RemoveFileIoOperation(filePath);
    }
}