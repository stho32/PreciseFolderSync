namespace Pfs.BL.Syncing.DirectoryWalkers;

public class DirectoryWalker : IDirectoryWalker
{
    public FileOrFolderCollection GetFilesAndFolders(string basePath)
    {
        var collection = new FileOrFolderCollection();

        foreach (var directory in Directory.GetDirectories(basePath, "*", SearchOption.AllDirectories))
        {
            var relativePath = directory.Substring(basePath.Length).TrimStart(Path.DirectorySeparatorChar);
            collection.Add(new FileOrFolder(false, directory, relativePath));
        }

        foreach (var file in Directory.GetFiles(basePath, "*", SearchOption.AllDirectories))
        {
            var relativePath = file.Substring(basePath.Length).TrimStart(Path.DirectorySeparatorChar);
            collection.Add(new FileOrFolder(true, file, relativePath));
        }

        return collection;
    }
}