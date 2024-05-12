using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileExistsIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;
    private readonly string sourceFilePath;

    public FileExistsIoCommand(string relativePath, string sourcePath)
    {
        relativePathInFrom = relativePath;
        sourceFilePath = sourcePath;
    }

    public string RelativePath => relativePathInFrom;

    public IoOperationResult Execute(string toBasePath, IIoHandler ioHandler)
    {
        string targetFilePath = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.CopyFile(sourceFilePath, targetFilePath);
    }
}