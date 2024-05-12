using NUnit.Framework;
using Pfs.BL.Syncing;

namespace Pfs.BL.Tests.Syncing;

[TestFixture]
public class FileOrFolderCollectionTests
{
    [Test]
    public void When_Single_Item_Is_Added_Then_Collection_Contains_That_Item()
    {
        var collection = new FileOrFolderCollection();
        var item = new FileOrFolder(true, "testAbsolutePath", "testRelativePath");

        collection.Add(item);

        Assert.That(collection.Items, Contains.Item(item));
    }

    [Test]
    public void When_Multiple_Items_Are_Added_Then_Collection_Contains_Those_Items()
    {
        var collection = new FileOrFolderCollection();
        var items = new List<FileOrFolder>
        {
            new(true, "testAbsolutePath1", "testRelativePath1"),
            new(false, "testAbsolutePath2", "testRelativePath2")
        };

        collection.AddRange(items);

        Assert.That(collection.Items, Is.EquivalentTo(items));
    }

}