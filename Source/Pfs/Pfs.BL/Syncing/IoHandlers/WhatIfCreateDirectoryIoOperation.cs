namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfCreateDirectoryIoOperation : IIoOperation
{
    public string Path { get; }
    public string RelativePath { get; }

    public WhatIfCreateDirectoryIoOperation(string path, string relativePath)
    {
        Path = path;
        RelativePath = relativePath;
    }

    public IoOperationResult Execute()
    {
        // This is a "what if" operation, so no actual operation is performed
        return new IoOperationResult(true, $"Would create directory: {Path}", this);
    }
}