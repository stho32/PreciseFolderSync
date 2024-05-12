namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfIoOperationFactory : IIoOperationFactory
{
    public IIoOperation CreateDirectory(string path)
    {
        return new WhatIfCreateDirectoryIoOperation(path);
    }

    public IIoOperation DeleteDirectory(string path)
    {
        return new WhatIfDeleteDirectoryIoOperation(path);
    }

    public IIoOperation CopyFile(string fromFilePath, string toFilePath)
    {
        return new WhatIfCopyFile(fromFilePath, toFilePath);
    }

    public IIoOperation RemoveFile(string filePath)
    {
        return new WhatIfRemoveFile(filePath);
    }
} 