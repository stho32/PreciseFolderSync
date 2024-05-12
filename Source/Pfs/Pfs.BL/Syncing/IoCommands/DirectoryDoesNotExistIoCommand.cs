﻿using Pfs.BL.Syncing.IoHandlers;

namespace Pfs.BL.Syncing.IoCommands;

public class DirectoryDoesNotExistIoCommand : IIoCommand
{
    private readonly string relativePathInFrom;
    private readonly IIoHandler ioHandler;

    public DirectoryDoesNotExistIoCommand(string relativePath, IIoHandler handler)
    {
        relativePathInFrom = relativePath;
        ioHandler = handler;
    }

    public IoOperationResult Execute(string toBasePath)
    {
        string targetDirectory = Path.Combine(toBasePath, relativePathInFrom);
        return ioHandler.DeleteDirectory(targetDirectory);
    }
}