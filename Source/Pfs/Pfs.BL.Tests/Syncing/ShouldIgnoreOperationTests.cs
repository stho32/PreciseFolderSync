using NUnit.Framework;
using Pfs.BL.Syncing;
using Pfs.BL.Syncing.DirectoryWalkers;
using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Tests.Syncing;

[TestFixture]
public class ShouldIgnoreOperationTests
{
    private string _testDir = null!;
    private string _sourceDir = null!;
    private string _destDir = null!;

    [SetUp]
    public void SetUp()
    {
        _testDir = Path.Combine(Path.GetTempPath(), "PfsIgnoreTests_" + Guid.NewGuid().ToString("N"));
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
    public void Ignored_File_Is_Not_Copied()
    {
        File.WriteAllText(Path.Combine(_sourceDir, "keep.txt"), "keep");
        File.WriteAllText(Path.Combine(_sourceDir, "skip.txt"), "skip");

        RunSyncWithIgnore(new[] { "skip.txt" });

        Assert.That(File.Exists(Path.Combine(_destDir, "keep.txt")), Is.True);
        Assert.That(File.Exists(Path.Combine(_destDir, "skip.txt")), Is.False);
    }

    [Test]
    public void Ignored_Directory_Contents_Are_Not_Copied()
    {
        var subDir = Path.Combine(_sourceDir, "logs");
        Directory.CreateDirectory(subDir);
        File.WriteAllText(Path.Combine(subDir, "app.log"), "log data");
        File.WriteAllText(Path.Combine(_sourceDir, "keep.txt"), "keep");

        RunSyncWithIgnore(new[] { @"logs\" });

        Assert.That(File.Exists(Path.Combine(_destDir, "keep.txt")), Is.True);
        Assert.That(File.Exists(Path.Combine(_destDir, "logs", "app.log")), Is.False);
    }

    [Test]
    public void Ignore_Is_Case_Insensitive()
    {
        File.WriteAllText(Path.Combine(_sourceDir, "ReadMe.txt"), "data");

        RunSyncWithIgnore(new[] { "readme.txt" });

        Assert.That(File.Exists(Path.Combine(_destDir, "ReadMe.txt")), Is.False);
    }

    private void RunSyncWithIgnore(IEnumerable<string> ignoreList)
    {
        var fromWalker = new DirectoryWalker();
        var toWalker = new DirectoryWalker();
        var synchronizer = new Synchronizer(fromWalker, toWalker);

        var commands = synchronizer.PrepareSync(_sourceDir, _destDir);

        var factory = new IoOperationFactory();
        var executor = new IoCommandListExecutor();
        var operations = executor.Prepare(commands, _destDir, factory);

        foreach (var op in operations)
        {
            if (!ShouldIgnoreOperation(op.RelativePath, ignoreList))
            {
                op.Execute();
            }
        }
    }

    private static bool ShouldIgnoreOperation(string relativePath, IEnumerable<string>? ignoreList)
    {
        if (ignoreList == null)
            return false;

        foreach (var ignoreItem in ignoreList)
        {
            if (ignoreItem.EndsWith("\\") && relativePath.StartsWith(ignoreItem, StringComparison.OrdinalIgnoreCase))
                return true;

            if (!ignoreItem.EndsWith("\\") && relativePath.Equals(ignoreItem, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
}
