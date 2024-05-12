namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfIoOperationFactory : IIoOperationFactory
{
    public IIoOperation CreateDirectory(string path, string relativePath)
    {
        return new WhatIfCreateDirectoryIoOperation(path, relativePath);
    }

    public IIoOperation DeleteDirectory(string path, string relativePath)
    {
        return new WhatIfDeleteDirectoryIoOperation(path, relativePath);
    }

    public IIoOperation CopyFile(string fromFilePath, string toFilePath, string relativePath)
    {
        return new WhatIfCopyFile(fromFilePath, toFilePath, relativePath);
    }

    public IIoOperation RemoveFile(string filePath, string relativePath)
    {
        return new WhatIfRemoveFile(filePath, relativePath);
    }
} 