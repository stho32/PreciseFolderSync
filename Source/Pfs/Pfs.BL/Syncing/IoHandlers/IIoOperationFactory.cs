namespace Pfs.BL.Syncing.IoHandlers;

public interface IIoOperationFactory
{
    IIoOperation CreateDirectory(string path, string relativePath);
    IIoOperation DeleteDirectory(string path, string relativePath);
    IIoOperation CopyFile(string fromFilePath, string toFilePath, string relativePath);
    IIoOperation RemoveFile(string filePath, string relativePath);
}