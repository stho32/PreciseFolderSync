using NUnit.Framework;
using Pfs.BL.Syncing;
using Pfs.BL.Syncing.DirectoryWalkers;
using Pfs.BL.Syncing.IoCommands;

namespace Pfs.BL.Tests.Syncing;

[TestFixture]
public class SynchronizerTests
{
    [Test]
    public void When_PrepareSync_Is_Called_With_Existing_File_Then_FileExists_Command_Is_Returned()
    {
        var fromCollection = new FileOrFolderCollection();
        fromCollection.Add(new FileOrFolder(true, "fromPath/file1", "file1"));

        var fromDirectoryWalker = new InMemoryDirectoryWalker(fromCollection);
        var toDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<FileExistsIoCommand>(), "The command should be a FileExistsIoCommand for file1");
        Assert.That(((FileExistsIoCommand)commands.Commands[0]).RelativePath, Is.EqualTo("file1"));
    }

    [Test]
    public void When_PrepareSync_Is_Called_With_Existing_Folder_Then_DirectoryExists_Command_Is_Returned()
    {
        var fromCollection = new FileOrFolderCollection();
        fromCollection.Add(new FileOrFolder(false, "fromPath/folder1", "folder1"));

        var fromDirectoryWalker = new InMemoryDirectoryWalker(fromCollection);
        var toDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<DirectoryExistsIoCommand>(), "The command should be a DirectoryExistsIoCommand for folder1");
        Assert.That(((DirectoryExistsIoCommand)commands.Commands[0]).RelativePath, Is.EqualTo("folder1"));
    }

    [Test]
    public void When_PrepareSync_Is_Called_With_Non_Existing_File_Then_FileDoesNotExist_Command_Is_Returned()
    {
        var toCollection = new FileOrFolderCollection();
        toCollection.Add(new FileOrFolder(true, "toPath/file2", "file2"));

        var fromDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());
        var toDirectoryWalker = new InMemoryDirectoryWalker(toCollection);

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<FileDoesNotExistIoCommand>(), "The command should be a FileDoesNotExistIoCommand for file2");
        Assert.That(((FileDoesNotExistIoCommand)commands.Commands[0]).RelativePath, Is.EqualTo("file2"));
    }

    [Test]
    public void When_PrepareSync_Is_Called_With_Non_Existing_Folder_Then_DirectoryDoesNotExist_Command_Is_Returned()
    {
        var toCollection = new FileOrFolderCollection();
        toCollection.Add(new FileOrFolder(false, "toPath/folder2", "folder2"));

        var fromDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());
        var toDirectoryWalker = new InMemoryDirectoryWalker(toCollection);

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<DirectoryDoesNotExistIoCommand>(), "The command should be a DirectoryDoesNotExistIoCommand for folder2");
        Assert.That(((DirectoryDoesNotExistIoCommand)commands.Commands[0]).RelativePath, Is.EqualTo("folder2"));
    }
}

