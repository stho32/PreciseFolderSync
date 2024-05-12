namespace Pfs.BL.Syncing.DirectoryWalkers;

public interface IDirectoryWalker
{
    FileOrFolderCollection GetFilesAndFolders(string basePath);
}