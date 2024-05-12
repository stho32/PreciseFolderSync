namespace Pfs.BL.Syncing.DirectoryWalkers;

public class InMemoryDirectoryWalker : IDirectoryWalker
{
    private readonly FileOrFolderCollection collection;

    public InMemoryDirectoryWalker(FileOrFolderCollection collection)
    {
        this.collection = collection;
    }

    public FileOrFolderCollection GetFilesAndFolders(string basePath)
    {
        return collection;
    }
}