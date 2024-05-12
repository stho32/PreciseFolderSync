using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileExistsIoCommand : IIoCommand
{
    public FileOrFolder FileOrFolder { get; }

    public FileExistsIoCommand(FileOrFolder fileOrFolder)
    {
        FileOrFolder = fileOrFolder;
    }

    public IoOperationResult Execute(string toBasePath, IIoHandler ioHandler)
    {
        string targetFilePath = Path.Combine(toBasePath, FileOrFolder.RelativePath);
        return ioHandler.CopyFile(FileOrFolder.AbsolutePath, targetFilePath);
    }
}