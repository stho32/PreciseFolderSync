﻿namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfCopyFile : IIoOperation
{
    public string FromPath { get; }
    public string ToPath { get; }
    public string RelativePath { get; }

    public WhatIfCopyFile(string fromPath, string toPath, string relativePath)
    {
        FromPath = fromPath;
        ToPath = toPath;
        RelativePath = relativePath;
    }

    public IoOperationResult Execute()
    {
        // This is a "what if" operation, so no actual operation is performed
        return new IoOperationResult(true, $"Would copy file from {FromPath} to {ToPath}", this);
    }
}