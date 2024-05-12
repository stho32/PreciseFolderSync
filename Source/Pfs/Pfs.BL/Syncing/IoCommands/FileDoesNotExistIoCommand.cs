using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileDoesNotExistIoCommand : IIoCommand
{
    public FileOrFolder FileOrFolder { get; }

    public FileDoesNotExistIoCommand(FileOrFolder fileOrFolder)
    {
        FileOrFolder = fileOrFolder;
    }

    public IIoOperation PrepareIoOperation(string toBasePath, IIoOperationFactory ioOperationFactory)
    {
        string targetFilePath = Path.Combine(toBasePath, FileOrFolder.RelativePath);
        return ioOperationFactory.RemoveFile(targetFilePath, FileOrFolder.RelativePath);
    }
}