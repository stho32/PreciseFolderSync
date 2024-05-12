using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileDoesNotExistIoCommand : IIoCommand
{
    public FileOrFolder FileOrFolder { get; }

    public FileDoesNotExistIoCommand(FileOrFolder fileOrFolder)
    {
        FileOrFolder = fileOrFolder;
    }

    public IoOperationResult Execute(string toBasePath, IIoHandler ioHandler)
    {
        string targetFilePath = Path.Combine(toBasePath, FileOrFolder.RelativePath);
        return ioHandler.RemoveFile(targetFilePath);
    }
}