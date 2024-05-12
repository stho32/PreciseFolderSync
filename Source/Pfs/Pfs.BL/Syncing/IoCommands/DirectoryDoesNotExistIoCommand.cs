using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class DirectoryDoesNotExistIoCommand : IIoCommand
{
    public FileOrFolder FileOrFolder { get; }

    public DirectoryDoesNotExistIoCommand(FileOrFolder fileOrFolder)
    {
        FileOrFolder = fileOrFolder;
    }

    public IIoOperation PrepareIoOperation(string toBasePath, IIoOperationFactory ioOperationFactory)
    {
        string targetDirectory = Path.Combine(toBasePath, FileOrFolder.RelativePath);
        return ioOperationFactory.DeleteDirectory(targetDirectory);
    }
}