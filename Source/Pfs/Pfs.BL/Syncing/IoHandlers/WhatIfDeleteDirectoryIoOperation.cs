namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfDeleteDirectoryIoOperation : IIoOperation
{
    public string Path { get; }

    public WhatIfDeleteDirectoryIoOperation(string path)
    {
        Path = path;
    }

    public IoOperationResult Execute()
    {
        // This is a "what if" operation, so no actual operation is performed
        return new IoOperationResult(true, $"Would delete directory: {Path}", this);
    }
}