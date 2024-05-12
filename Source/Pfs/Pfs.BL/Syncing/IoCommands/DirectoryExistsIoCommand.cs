using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class DirectoryExistsIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;
    private readonly IIoHandler ioHandler;

    public DirectoryExistsIoCommand(string relativePath, IIoHandler handler)
    {
        relativePathInFrom = relativePath;
        ioHandler = handler;
    }

    public IoOperationResult Execute(string toBasePath)
    {
        string targetDirectory = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.CreateDirectory(targetDirectory);
    }
}