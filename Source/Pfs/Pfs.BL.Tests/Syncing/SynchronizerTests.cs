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
        var file1 = new FileOrFolder(true, "fromPath/file1", "file1");
        fromCollection.Add(file1);

        var fromDirectoryWalker = new InMemoryDirectoryWalker(fromCollection);
        var toDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<FileExistsIoCommand>(), "The command should be a FileExistsIoCommand for file1");
        Assert.That(commands.Commands[0].FileOrFolder, Is.EqualTo(file1), "The FileOrFolder object should be the same as the one added to the fromCollection");
    }

    [Test]
    public void When_PrepareSync_Is_Called_With_Existing_Folder_Then_DirectoryExists_Command_Is_Returned()
    {
        var fromCollection = new FileOrFolderCollection();
        var folder1 = new FileOrFolder(false, "fromPath/folder1", "folder1");
        fromCollection.Add(folder1);

        var fromDirectoryWalker = new InMemoryDirectoryWalker(fromCollection);
        var toDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<DirectoryExistsIoCommand>(), "The command should be a DirectoryExistsIoCommand for folder1");
        Assert.That(commands.Commands[0].FileOrFolder, Is.EqualTo(folder1), "The FileOrFolder object should be the same as the one added to the fromCollection");
    }

    [Test]
    public void When_PrepareSync_Is_Called_With_Non_Existing_File_Then_FileDoesNotExist_Command_Is_Returned()
    {
        var toCollection = new FileOrFolderCollection();
        var file2 = new FileOrFolder(true, "toPath/file2", "file2");
        toCollection.Add(file2);

        var fromDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());
        var toDirectoryWalker = new InMemoryDirectoryWalker(toCollection);

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<FileDoesNotExistIoCommand>(), "The command should be a FileDoesNotExistIoCommand for file2");
        Assert.That(commands.Commands[0].FileOrFolder, Is.EqualTo(file2), "The FileOrFolder object should be the same as the one added to the toCollection");
    }

    [Test]
    public void When_PrepareSync_Is_Called_With_Non_Existing_Folder_Then_DirectoryDoesNotExist_Command_Is_Returned()
    {
        var toCollection = new FileOrFolderCollection();
        var folder2 = new FileOrFolder(false, "toPath/folder2", "folder2");
        toCollection.Add(folder2);

        var fromDirectoryWalker = new InMemoryDirectoryWalker(new FileOrFolderCollection());
        var toDirectoryWalker = new InMemoryDirectoryWalker(toCollection);

        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync("fromPath", "toPath");

        Assert.That(commands.Commands.Count, Is.EqualTo(1), "There should be 1 command");
        Assert.That(commands.Commands[0], Is.TypeOf<DirectoryDoesNotExistIoCommand>(), "The command should be a DirectoryDoesNotExistIoCommand for folder2");
        Assert.That(commands.Commands[0].FileOrFolder, Is.EqualTo(folder2), "The FileOrFolder object should be the same as the one added to the toCollection");
    }
}


