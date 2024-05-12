using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public interface IIoCommand
{
    string RelativePath { get; }

    IoOperationResult Execute(string toBasePath, IIoHandler ioHandler);
}
