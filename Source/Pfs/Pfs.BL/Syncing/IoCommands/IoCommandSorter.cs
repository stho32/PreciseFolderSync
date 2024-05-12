using Pfs.BL.Syncing.IoCommands;

public static class IoCommandSorter
{
    public static void SortCommands(List<IIoCommand> commands)
    {
        commands.Sort((x, y) =>
        {
            // First, sort by the type of command
            if (x is DirectoryExistsIoCommand && y is DirectoryDoesNotExistIoCommand)
            {
                return -1; // Create directories come before delete directories
            }

            if (x is DirectoryDoesNotExistIoCommand && y is DirectoryExistsIoCommand)
            {
                return 1; // Delete directories come after create directories
            }

            if (x is FileExistsIoCommand && y is FileDoesNotExistIoCommand)
            {
                return -1; // Create files come before delete files
            }

            if (x is FileDoesNotExistIoCommand && y is FileExistsIoCommand)
            {
                return 1; // Delete files come after create files
            }

            // If both commands are of the same type, sort by relative path
            // For create commands, sort in ascending order
            // For delete commands, sort in descending order
            if (x is DirectoryExistsIoCommand || x is FileExistsIoCommand)
            {
                return string.Compare(x.FileOrFolder.RelativePath, y.FileOrFolder.RelativePath, StringComparison.Ordinal);
            }

            return string.Compare(y.FileOrFolder.RelativePath, x.FileOrFolder.RelativePath, StringComparison.Ordinal);
        });
    }
}