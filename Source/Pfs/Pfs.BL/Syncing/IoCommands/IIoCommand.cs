using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public interface IIoCommand
{
    IoOperationResult Execute(string toBasePath, IIoHandler ioHandler);
}