namespace Pfs.BL.Syncing.IoHandlers;

public record IoOperationResult(bool IsSuccess, string Message, IIoOperation Operation);