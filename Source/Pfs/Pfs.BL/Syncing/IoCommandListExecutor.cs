using Pfs.BL.Syncing.IoCommands;
using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing;

public class IoCommandListExecutor
{
    public IIoOperation[] Prepare(IoCommandList commands, string toBasePath,IIoOperationFactory ioOperationFactory)
    {
        commands.Sort();

        var results = new List<IIoOperation>();
        foreach (var command in commands.Commands)
        {
            var ioOperation = command.PrepareIoOperation(toBasePath, ioOperationFactory);
            results.Add(ioOperation);
        }

        return results.ToArray();
    }
}