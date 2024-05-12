namespace Pfs.BL.Syncing.IoCommands;

public class IoCommandList
{
    private readonly List<IIoCommand> commands = new();

    public IoCommandList(List<IIoCommand>? ioCommands = null)
    {
        if (ioCommands != null)
            commands.AddRange(ioCommands);
    }

    public IReadOnlyList<IIoCommand> Commands => commands;

    public void Add(IIoCommand command)
    {
        commands.Add(command);
    }

    public void Sort()
    {
        IoCommandSorter.SortCommands(commands);
    }
}