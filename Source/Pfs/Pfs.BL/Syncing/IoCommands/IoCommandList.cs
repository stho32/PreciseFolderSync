namespace Pfs.BL.Syncing.IoCommands;

public class IoCommandList
{
    private readonly List<IIoCommand> commands = new();

    public IReadOnlyList<IIoCommand> Commands => commands;

    public void Add(IIoCommand command)
    {
        commands.Add(command);
    }

}