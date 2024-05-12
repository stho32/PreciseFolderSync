﻿using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class FileExistsIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;
    private readonly string sourceFilePath;
    private readonly IIoHandler ioHandler;

    public FileExistsIoCommand(string relativePath, string sourcePath, IIoHandler handler)
    {
        relativePathInFrom = relativePath;
        sourceFilePath = sourcePath;
        ioHandler = handler;
    }

    public IoOperationResult Execute(string toBasePath)
    {
        string targetFilePath = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.CopyFile(sourceFilePath, targetFilePath);
    }
}