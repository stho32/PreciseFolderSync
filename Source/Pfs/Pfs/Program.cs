using Pfs.BL.CommandLineArguments;
using Pfs.BL.Syncing.DirectoryWalkers;
using Pfs.BL.Syncing.IoHandlers;
using Pfs.BL.Syncing;

class Program
{
    static void Main(string[] args)
    {
        var parseResult = CommandLineArgumentsParser.ParseCommandLineArguments(args);

        if (!parseResult.IsSuccess)
        {
            Console.WriteLine(parseResult.Output);
            Environment.Exit(parseResult.ExitCode);
        }

        var options = parseResult.Options;

        IDirectoryWalker fromDirectoryWalker = new DirectoryWalker();
        IDirectoryWalker toDirectoryWalker = new DirectoryWalker();

        Synchronize(fromDirectoryWalker, toDirectoryWalker, options);
    }

    private static int Synchronize(IDirectoryWalker fromDirectoryWalker, IDirectoryWalker toDirectoryWalker,
   CommandLineOptions options)
    {
        var synchronizer = new Synchronizer(fromDirectoryWalker, toDirectoryWalker);

        var commands = synchronizer.PrepareSync(options.FromPath, options.ToPath);

        IIoOperationFactory ioOperationFactory = options.WhatIf ? new WhatIfIoOperationFactory() : new IoOperationFactory();

        var executor = new IoCommandListExecutor();
        var ioOperations = executor.Prepare(commands, options.ToPath, ioOperationFactory);

        var failedOperations = new List<IIoOperation>();
        var maxRetries = 5;
        var retryCount = 0;

        while (retryCount < maxRetries)
        {
            failedOperations.Clear();
            int operationIndex = 0;

            foreach (var operation in ioOperations)
            {
                if (ShouldIgnoreOperation(operation.RelativePath, options.Ignore))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray; // Silver color
                    Console.WriteLine($"{DateTime.Now} - IGNORED - {operation.RelativePath}");
                    Console.ResetColor();
                    continue;
                }

                operationIndex++;
                var progressInfo = $"{operationIndex}/{ioOperations.Length}";

                var result = operation.Execute();

                if (result.IsSuccess)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{DateTime.Now} - SUCCESS - {progressInfo} - {result.Message}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{DateTime.Now} - FAILURE - {progressInfo} - {result.Message}");
                    failedOperations.Add(operation);
                }
            }

            if (failedOperations.Count == 0)
            {
                Console.ResetColor();
                return 0; // Success
            }

            retryCount++;
            if (retryCount < maxRetries)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{DateTime.Now} - NOTE - Retry {retryCount} will occur after a 5-second wait.");
                Console.ResetColor();
                Thread.Sleep(5000); // Wait for 5 seconds
                ioOperations = failedOperations.ToArray();
            }
        }

        Console.ResetColor();
        return 1; // Failure
    }

    private static bool ShouldIgnoreOperation(string relativePath, IEnumerable<string>? ignoreList)
    {
        if (ignoreList == null)
        {
            return false;
        }

        foreach (var ignoreItem in ignoreList)
        {
            if (ignoreItem.EndsWith("\\") && relativePath.StartsWith(ignoreItem, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (!ignoreItem.EndsWith("\\") && relativePath.Equals(ignoreItem, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
