namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfRemoveFile : IIoOperation
{
    public string Path { get; }

    public WhatIfRemoveFile(string path)
    {
        Path = path;
    }

    public IoOperationResult Execute()
    {
        // This is a "what if" operation, so no actual operation is performed
        return new IoOperationResult(true, $"Would remove file: {Path}", this);
    }
}