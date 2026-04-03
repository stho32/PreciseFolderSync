using NUnit.Framework;
using Pfs.BL.Syncing;
using Pfs.BL.Syncing.DirectoryWalkers;
using Pfs.BL.Syncing.IoCommands;
using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Tests.Syncing;

[TestFixture]
public class IoCommandListExecutorTests
{
    [Test]
    public void Prepare_Returns_Correct_Number_Of_Operations()
    {
        var fromCollection = new FileOrFolderCollection();
        fromCollection.Add(new FileOrFolder(false, "C:\\from\\dir1", "dir1"));
        fromCollection.Add(new FileOrFolder(true, "C:\\from\\file1.txt", "file1.txt"));

        var toCollection = new FileOrFolderCollection();
        toCollection.Add(new FileOrFolder(true, "C:\\to\\old.txt", "old.txt"));

        var fromWalker = new InMemoryDirectoryWalker(fromCollection);
        var toWalker = new InMemoryDirectoryWalker(toCollection);

        var synchronizer = new Synchronizer(fromWalker, toWalker);
        var commands = synchronizer.PrepareSync("C:\\from", "C:\\to");

        var executor = new IoCommandListExecutor();
        var operations = executor.Prepare(commands, "C:\\to", new WhatIfIoOperationFactory());

        Assert.That(operations.Length, Is.EqualTo(3));
    }

    [Test]
    public void Prepare_Sorts_Creates_Before_Deletes_Of_Same_Type()
    {
        var commands = new IoCommandList(new List<IIoCommand>
        {
            new DirectoryDoesNotExistIoCommand(new FileOrFolder(false, "C:\\to\\olddir", "olddir")),
            new DirectoryExistsIoCommand(new FileOrFolder(false, "C:\\from\\newdir", "newdir"))
        });

        var executor = new IoCommandListExecutor();
        var operations = executor.Prepare(commands, "C:\\to", new WhatIfIoOperationFactory());

        var results = operations.Select(op => op.Execute()).ToArray();

        Assert.That(results[0].Message, Does.Contain("create directory").IgnoreCase);
        Assert.That(results[1].Message, Does.Contain("delete directory").IgnoreCase);
    }

    [Test]
    public void WhatIf_Operations_All_Succeed_Without_Side_Effects()
    {
        var commands = new IoCommandList(new List<IIoCommand>
        {
            new DirectoryExistsIoCommand(new FileOrFolder(false, "C:\\from\\dir1", "dir1")),
            new FileExistsIoCommand(new FileOrFolder(true, "C:\\from\\f.txt", "f.txt")),
            new FileDoesNotExistIoCommand(new FileOrFolder(true, "C:\\to\\old.txt", "old.txt")),
            new DirectoryDoesNotExistIoCommand(new FileOrFolder(false, "C:\\to\\olddir", "olddir"))
        });

        var executor = new IoCommandListExecutor();
        var operations = executor.Prepare(commands, "C:\\to", new WhatIfIoOperationFactory());

        var results = operations.Select(op => op.Execute()).ToArray();

        Assert.That(results.All(r => r.IsSuccess), Is.True);
        Assert.That(results.All(r => r.Message.StartsWith("Would")), Is.True);
    }
}
