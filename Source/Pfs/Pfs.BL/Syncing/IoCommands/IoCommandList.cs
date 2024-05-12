namespace Pfs.BL.Syncing.IoCommands;

public class IoCommandList
{
    private readonly List<IIoCommand> commands = new();

    public IReadOnlyList<IIoCommand> Commands => commands;

    public void Add(IIoCommand command)
    {
        commands.Add(command);
    }

    public void Sort()
    {
        commands.Sort((x, y) =>
        {
            // Sort by whether the command is associated with a file or a directory
            int fileTypeComparison = x.FileOrFolder.IsFile.CompareTo(y.FileOrFolder.IsFile);
            if (fileTypeComparison != 0)
            {
                return fileTypeComparison;
            }

            // If both commands are associated with the same type of file system object, sort by relative path
            return string.Compare(x.FileOrFolder.RelativePath, y.FileOrFolder.RelativePath, StringComparison.Ordinal);
        });
    }
}
