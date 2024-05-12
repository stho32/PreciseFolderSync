namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfCreateDirectoryIoOperation : IIoOperation
{
    public string Path { get; }

    public WhatIfCreateDirectoryIoOperation(string path)
    {
        Path = path;
    }

    public IoOperationResult Execute()
    {
        // This is a "what if" operation, so no actual operation is performed
        return new IoOperationResult(true, $"Would create directory: {Path}", this);
    }
}