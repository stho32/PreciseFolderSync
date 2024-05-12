using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public interface IIoCommand
{
    FileOrFolder FileOrFolder { get; }

    IIoOperation PrepareIoOperation(string toBasePath, IIoOperationFactory ioOperationFactory);
}
