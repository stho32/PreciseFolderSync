namespace Pfs.BL.Syncing.IoHandlers;

public interface IIoHandler
{
    IoOperationResult CreateDirectory(string path);
    IoOperationResult DeleteDirectory(string path);
    IoOperationResult CopyFile(string fromFilePath, string toFilePath);
    IoOperationResult RemoveFile(string filePath);
}