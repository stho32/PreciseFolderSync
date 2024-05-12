using NUnit.Framework;
using Pfs.BL.Syncing.IoCommands;
using Pfs.BL.Syncing;

[TestFixture]
public class IoCommandSorterTests
{
    [Test]
    public void DirectoriesToBeCreatedAreSortedToTheTopInAscendingOrder()
    {
        var commands = new List<IIoCommand>
        {
            new DirectoryExistsIoCommand(new FileOrFolder(false, "C:\\Test\\Sub2", "\\Sub2")),
            new DirectoryExistsIoCommand(new FileOrFolder(false, "C:\\Test", "")),
            new DirectoryExistsIoCommand(new FileOrFolder(false, "C:\\Test\\Sub1", "\\Sub1"))
        };

        IoCommandSorter.SortCommands(commands);

        Assert.That(commands[0].FileOrFolder.RelativePath, Is.EqualTo(""));
        Assert.That(commands[1].FileOrFolder.RelativePath, Is.EqualTo("\\Sub1"));
        Assert.That(commands[2].FileOrFolder.RelativePath, Is.EqualTo("\\Sub2"));
    }

    [Test]
    public void DirectoriesToBeDeletedAreSortedToTheEndInDescendingOrder()
    {
        var commands = new List<IIoCommand>
        {
            new DirectoryDoesNotExistIoCommand(new FileOrFolder(false, "C:\\Test", "")),
            new DirectoryDoesNotExistIoCommand(new FileOrFolder(false, "C:\\Test\\Sub1", "\\Sub1")),
            new DirectoryDoesNotExistIoCommand(new FileOrFolder(false, "C:\\Test\\Sub2", "\\Sub2"))
        };

        IoCommandSorter.SortCommands(commands);

        Assert.That(commands[0].FileOrFolder.RelativePath, Is.EqualTo("\\Sub2"));
        Assert.That(commands[1].FileOrFolder.RelativePath, Is.EqualTo("\\Sub1"));
        Assert.That(commands[2].FileOrFolder.RelativePath, Is.EqualTo(""));
    }

    [Test]
    public void FilesToBeCreatedAreBelowDirectoryCreationsAndFilesToBeDeletedAreLast()
    {
        var commands = new List<IIoCommand>
        {
            new FileDoesNotExistIoCommand(new FileOrFolder(true, "C:\\Test\\file1.txt", "\\file1.txt")),
            new DirectoryExistsIoCommand(new FileOrFolder(false, "C:\\Test", "")),
            new FileExistsIoCommand(new FileOrFolder(true, "C:\\Test\\file2.txt", "\\file2.txt")),
            new DirectoryDoesNotExistIoCommand(new FileOrFolder(false, "C:\\Test\\Sub1", "\\Sub1"))
        };

        IoCommandSorter.SortCommands(commands);

        Assert.That(commands[0], Is.InstanceOf<DirectoryExistsIoCommand>());
        Assert.That(commands[1], Is.InstanceOf<FileExistsIoCommand>());
        Assert.That(commands[2], Is.InstanceOf<FileDoesNotExistIoCommand>());
        Assert.That(commands[3], Is.InstanceOf<DirectoryDoesNotExistIoCommand>());
    }
}
