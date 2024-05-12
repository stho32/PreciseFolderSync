using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class DirectoryExistsIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;

    public DirectoryExistsIoCommand(string relativePath)
    {
        relativePathInFrom = relativePath;
    }

    public string RelativePath => relativePathInFrom;

    public IoOperationResult Execute(string toBasePath, IIoHandler ioHandler)
    {
        string targetDirectory = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.CreateDirectory(targetDirectory);
    }
}