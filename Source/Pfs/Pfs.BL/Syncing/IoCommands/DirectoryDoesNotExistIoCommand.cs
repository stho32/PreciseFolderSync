using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class DirectoryDoesNotExistIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;

    public DirectoryDoesNotExistIoCommand(string relativePath)
    {
        relativePathInFrom = relativePath;
    }

    public IoOperationResult Execute(string toBasePath, IIoHandler ioHandler)
    {
        string targetDirectory = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.DeleteDirectory(targetDirectory);
    }
}