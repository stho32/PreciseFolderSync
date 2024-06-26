﻿namespace Pfs.BL.Syncing.IoHandlers;

public interface IIoOperation
{
    IoOperationResult Execute();
    string RelativePath { get; }
}