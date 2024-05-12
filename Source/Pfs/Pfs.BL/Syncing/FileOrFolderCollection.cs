namespace Pfs.BL.Syncing;

public class FileOrFolderCollection
{
    private List<FileOrFolder> items = new();

    public void Add(FileOrFolder item)
    {
        items.Add(item);
    }

    public void AddRange(IEnumerable<FileOrFolder> collection)
    {
        items.AddRange(collection);
    }

    public IReadOnlyList<FileOrFolder> Items => items.AsReadOnly();
}