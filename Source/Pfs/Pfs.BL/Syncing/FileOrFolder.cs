namespace Pfs.BL.Syncing;

public record FileOrFolder(bool IsFile, string AbsolutePath, string RelativePath);
