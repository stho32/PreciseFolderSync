namespace Pfs.BL.Syncing.IoHandlers;

public interface IIoOperationFactory
{
    IIoOperation CreateDirectory(string path);
    IIoOperation DeleteDirectory(string path);
    IIoOperation CopyFile(string fromFilePath, string toFilePath);
    IIoOperation RemoveFile(string filePath);
}