using NUnit.Framework;
using Pfs.BL.Syncing;
using Pfs.BL.Syncing.DirectoryWalkers;
using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Tests.Syncing;

[TestFixture]
public class IntegrationTests
{
    private string _testDir = null!;
    private string _sourceDir = null!;
    private string _destDir = null!;

    [SetUp]
    public void SetUp()
    {
        _testDir = Path.Combine(Path.GetTempPath(), "PfsIntegrationTests_" + Guid.NewGuid().ToString("N"));
        _sourceDir = Path.Combine(_testDir, "source");
        _destDir = Path.Combine(_testDir, "dest");
        Directory.CreateDirectory(_sourceDir);
        Directory.CreateDirectory(_destDir);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_testDir))
            Directory.Delete(_testDir, true);
    }

    [Test]
    public void Sync_Copies_New_Files_To_Destination()
    {
        File.WriteAllText(Path.Combine(_sourceDir, "file1.txt"), "hello");
        File.WriteAllText(Path.Combine(_sourceDir, "file2.txt"), "world");

        RunSync();

        Assert.That(File.Exists(Path.Combine(_destDir, "file1.txt")), Is.True);
        Assert.That(File.Exists(Path.Combine(_destDir, "file2.txt")), Is.True);
        Assert.That(File.ReadAllText(Path.Combine(_destDir, "file1.txt")), Is.EqualTo("hello"));
        Assert.That(File.ReadAllText(Path.Combine(_destDir, "file2.txt")), Is.EqualTo("world"));
    }

    [Test]
    public void Sync_Creates_Subdirectories_In_Destination()
    {
        var subDir = Path.Combine(_sourceDir, "subdir");
        Directory.CreateDirectory(subDir);
        File.WriteAllText(Path.Combine(subDir, "nested.txt"), "nested content");

        RunSync();

        Assert.That(Directory.Exists(Path.Combine(_destDir, "subdir")), Is.True);
        Assert.That(File.Exists(Path.Combine(_destDir, "subdir", "nested.txt")), Is.True);
        Assert.That(File.ReadAllText(Path.Combine(_destDir, "subdir", "nested.txt")), Is.EqualTo("nested content"));
    }

    [Test]
    public void Sync_Deletes_Files_In_Destination_Not_In_Source()
    {
        File.WriteAllText(Path.Combine(_destDir, "obsolete.txt"), "old content");

        RunSync();

        Assert.That(File.Exists(Path.Combine(_destDir, "obsolete.txt")), Is.False);
    }

    [Test]
    public void Sync_Deletes_Directories_In_Destination_Not_In_Source()
    {
        var obsoleteDir = Path.Combine(_destDir, "olddir");
        Directory.CreateDirectory(obsoleteDir);
        File.WriteAllText(Path.Combine(obsoleteDir, "old.txt"), "old");

        RunSync();

        Assert.That(Directory.Exists(Path.Combine(_destDir, "olddir")), Is.False);
    }

    [Test]
    public void Sync_Overwrites_Existing_Files_With_Updated_Content()
    {
        File.WriteAllText(Path.Combine(_sourceDir, "data.txt"), "new content");
        File.WriteAllText(Path.Combine(_destDir, "data.txt"), "old content");

        RunSync();

        Assert.That(File.ReadAllText(Path.Combine(_destDir, "data.txt")), Is.EqualTo("new content"));
    }

    [Test]
    public void Sync_Handles_Deeply_Nested_Directories()
    {
        var deepPath = Path.Combine(_sourceDir, "a", "b", "c");
        Directory.CreateDirectory(deepPath);
        File.WriteAllText(Path.Combine(deepPath, "deep.txt"), "deep");

        RunSync();

        Assert.That(File.Exists(Path.Combine(_destDir, "a", "b", "c", "deep.txt")), Is.True);
    }

    [Test]
    public void Sync_Handles_Empty_Source_Clears_Destination()
    {
        File.WriteAllText(Path.Combine(_destDir, "existing.txt"), "data");
        var subDir = Path.Combine(_destDir, "sub");
        Directory.CreateDirectory(subDir);

        RunSync();

        Assert.That(File.Exists(Path.Combine(_destDir, "existing.txt")), Is.False);
        Assert.That(Directory.Exists(Path.Combine(_destDir, "sub")), Is.False);
    }

    [Test]
    public void WhatIf_Mode_Does_Not_Modify_Destination()
    {
        File.WriteAllText(Path.Combine(_sourceDir, "new.txt"), "data");
        File.WriteAllText(Path.Combine(_destDir, "obsolete.txt"), "old");

        RunSync(whatIf: true);

        Assert.That(File.Exists(Path.Combine(_destDir, "new.txt")), Is.False, "WhatIf should not copy files");
        Assert.That(File.Exists(Path.Combine(_destDir, "obsolete.txt")), Is.True, "WhatIf should not delete files");
    }

    private void RunSync(bool whatIf = false)
    {
        var fromWalker = new DirectoryWalker();
        var toWalker = new DirectoryWalker();
        var synchronizer = new Synchronizer(fromWalker, toWalker);

        var commands = synchronizer.PrepareSync(_sourceDir, _destDir);

        IIoOperationFactory factory = whatIf
            ? new WhatIfIoOperationFactory()
            : new IoOperationFactory();

        var executor = new IoCommandListExecutor();
        var operations = executor.Prepare(commands, _destDir, factory);

        foreach (var op in operations)
        {
            op.Execute();
        }
    }
}
