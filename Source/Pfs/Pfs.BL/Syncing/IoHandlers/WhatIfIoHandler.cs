namespace Pfs.BL.Syncing.IoHandlers;

public class WhatIfIoHandler : IIoHandler
{
    public IoOperationResult CreateDirectory(string path)
    {
        return new IoOperationResult(true, $"Would create directory: {path}");
    }

    public IoOperationResult DeleteDirectory(string path)
    {
        return new IoOperationResult(true, $"Would delete directory: {path}");
    }

    public IoOperationResult CopyFile(string fromFilePath, string toFilePath)
    {
        return new IoOperationResult(true, $"Would copy file from {fromFilePath} to {toFilePath}");
    }

    public IoOperationResult RemoveFile(string filePath)
    {
        return new IoOperationResult(true, $"Would delete file: {filePath}");
    }
}