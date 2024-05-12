namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfRemoveFile : IIoOperation
{
    public string Path { get; }
    public string RelativePath { get; }

    public WhatIfRemoveFile(string path, string relativePath)
    {
        Path = path;
        RelativePath = relativePath;
    }

    public IoOperationResult Execute()
    {
        // This is a "what if" operation, so no actual operation is performed
        return new IoOperationResult(true, $"Would remove file: {Path}", this);
    }
}