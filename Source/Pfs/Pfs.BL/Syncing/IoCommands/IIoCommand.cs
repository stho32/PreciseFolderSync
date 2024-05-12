using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public interface IIoCommand
{
    FileOrFolder FileOrFolder { get; }

    IoOperationResult Execute(string toBasePath, IIoHandler ioHandler);
}
