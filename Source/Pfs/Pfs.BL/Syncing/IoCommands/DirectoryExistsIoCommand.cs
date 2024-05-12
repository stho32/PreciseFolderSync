using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class DirectoryExistsIoCommand : IIoCommand
{
    public FileOrFolder FileOrFolder { get; }

    public DirectoryExistsIoCommand(FileOrFolder fileOrFolder)
    {
        FileOrFolder = fileOrFolder;
    }

    public IIoOperation PrepareIoOperation(string toBasePath, IIoOperationFactory ioOperationFactory)
    {
        string targetDirectory = Path.Combine(toBasePath, FileOrFolder.RelativePath);
        return ioOperationFactory.CreateDirectory(targetDirectory, FileOrFolder.RelativePath);
    }
}