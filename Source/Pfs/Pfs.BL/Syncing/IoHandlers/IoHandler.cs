namespace Pfs.BL.Syncing.IoHandlers;

public class IoHandler : IIoHandler
{
    public IoOperationResult CreateDirectory(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return new IoOperationResult(true, $"Created directory: {path}");
            }
            return new IoOperationResult(false, $"Directory already exists: {path}");
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error creating directory {path}: {ex.Message}");
        }
    }

    public IoOperationResult DeleteDirectory(string path)
    {
        try
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                return new IoOperationResult(true, $"Deleted directory: {path}");
            }
            return new IoOperationResult(false, $"Directory does not exist: {path}");
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error deleting directory {path}: {ex.Message}");
        }
    }

    public IoOperationResult CopyFile(string fromFilePath, string toFilePath)
    {
        try
        {
            File.Copy(fromFilePath, toFilePath, true);
            return new IoOperationResult(true, $"Copied file from {fromFilePath} to {toFilePath}");
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error copying file from {fromFilePath} to {toFilePath}: {ex.Message}");
        }
    }

    public IoOperationResult RemoveFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return new IoOperationResult(true, $"Deleted file: {filePath}");
            }
            return new IoOperationResult(false, $"File does not exist: {filePath}");
        }
        catch (Exception ex)
        {
            return new IoOperationResult(false, $"Error removing file {filePath}: {ex.Message}");
        }
    }
}