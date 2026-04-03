using Pfs.BL.Syncing.DirectoryWalkers;
using Pfs.BL.Syncing.IoCommands;

namespace Pfs.BL.Syncing;

public class Synchronizer
{
    private readonly IDirectoryWalker _fromDirectoryWalker;
    private readonly IDirectoryWalker _toDirectoryWalker;

    public Synchronizer(IDirectoryWalker fromDirectoryWalker, IDirectoryWalker toDirectoryWalker)
    {
        _fromDirectoryWalker = fromDirectoryWalker;
        _toDirectoryWalker = toDirectoryWalker;
    }

    public IoCommandList PrepareSync(string fromPath, string toPath)
    {
        var from = _fromDirectoryWalker.GetFilesAndFolders(fromPath);
        var to = _toDirectoryWalker.GetFilesAndFolders(toPath);

        var commands = new IoCommandList();

        foreach (var item in from.Items)
        {
            if (item.IsFile)
            {
                commands.Add(new FileExistsIoCommand(item));
            }
            else
            {
                commands.Add(new DirectoryExistsIoCommand(item));
            }
        }

        var fromPaths = new HashSet<string>(
            from.Items.Select(i => i.RelativePath),
            StringComparer.OrdinalIgnoreCase);

        foreach (var item in to.Items)
        {
            if (!fromPaths.Contains(item.RelativePath))
            {
                if (item.IsFile)
                {
                    commands.Add(new FileDoesNotExistIoCommand(item));
                }
                else
                {
                    commands.Add(new DirectoryDoesNotExistIoCommand(item));
                }
            }
        }

        return commands;
    }
}
