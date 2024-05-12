using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class DirectoryExistsIoCommand : IIoCommand
{
    public FileOrFolder FileOrFolder { get; }

    public DirectoryExistsIoCommand(FileOrFolder fileOrFolder)
    {
        FileOrFolder = fileOrFolder;
    }

    public IoOperationResult Execute(string toBasePath, IIoHandler ioHandler)
    {
        string targetDirectory = Path.Combine(toBasePath, FileOrFolder.RelativePath);
        return ioHandler.CreateDirectory(targetDirectory);
    }
}