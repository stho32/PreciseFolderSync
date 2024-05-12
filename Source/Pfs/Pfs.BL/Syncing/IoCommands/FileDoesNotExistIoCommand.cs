using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileDoesNotExistIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;
    private readonly IIoHandler ioHandler;

    public FileDoesNotExistIoCommand(string relativePath, IIoHandler handler)
    {
        relativePathInFrom = relativePath;
        ioHandler = handler;
    }

    public IoOperationResult Execute(string toBasePath)
    {
        string targetFilePath = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.RemoveFile(targetFilePath);
    }
}