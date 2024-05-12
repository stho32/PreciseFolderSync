using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileDoesNotExistIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;

    public FileDoesNotExistIoCommand(string relativePath)
    {
        relativePathInFrom = relativePath;
    }

    public string RelativePath => relativePathInFrom;

    public IoOperationResult Execute(string toBasePath, IIoHandler ioHandler)
    {
        string targetFilePath = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.RemoveFile(targetFilePath);
    }
}