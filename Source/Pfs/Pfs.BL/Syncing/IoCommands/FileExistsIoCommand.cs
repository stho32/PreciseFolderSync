using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileExistsIoCommand : IIoCommand
{
    public FileOrFolder FileOrFolder { get; }

    public FileExistsIoCommand(FileOrFolder fileOrFolder)
    {
        FileOrFolder = fileOrFolder;
    }

    public IIoOperation PrepareIoOperation(string toBasePath, IIoOperationFactory ioOperationFactory)
    {
        string targetFilePath = Path.Combine(toBasePath, FileOrFolder.RelativePath);
        return ioOperationFactory.CopyFile(FileOrFolder.AbsolutePath, targetFilePath, FileOrFolder.RelativePath);
    }
}