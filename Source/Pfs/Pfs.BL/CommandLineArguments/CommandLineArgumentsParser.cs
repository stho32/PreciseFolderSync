using System.Reflection;
using CommandLine;
using CommandLine.Text;

namespace Pfs.BL.CommandLineArguments;

public record ParseResult(CommandLineOptions? Options, string Output, bool IsSuccess, int ExitCode);

public static class CommandLineArgumentsParser
{
    private static string GetVersion()
    {
        return Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3) ?? "0.0.0";
    }

    public static ParseResult ParseCommandLineArguments(string[] args)
    {
        CommandLineOptions? options = null;
        string output = "";
        bool isSuccess = true;
        int exitCode = 0;

        var parser = new Parser(with => with.HelpWriter = null);

        var parserResult = parser.ParseArguments<CommandLineOptions>(args);

        parserResult
            .WithParsed(parsedOptions => options = parsedOptions)
            .WithNotParsed(errors =>
            {
                var errorsArray = errors as Error[] ?? errors.ToArray();

                if (errorsArray.Any(e => e.Tag == ErrorType.VersionRequestedError))
                {
                    output = $"PreciseFolderSync Version {GetVersion()}";
                    exitCode = 0;
                }
                else if (errorsArray.Any(e => e.Tag == ErrorType.HelpRequestedError))
                {
                    output = HelpText.AutoBuild(parserResult, CustomizeHelpText).ToString();
                    exitCode = 0;
                }
                else
                {
                    output = HelpText.AutoBuild(parserResult, CustomizeHelpText).ToString();
                    exitCode = 1;
                }

                isSuccess = false;
            });

        return new ParseResult(options, output, isSuccess, exitCode);
    }


    private static HelpText CustomizeHelpText(HelpText helpText)
    {
        helpText.AdditionalNewLineAfterOption = true;
        helpText.Heading = $"PreciseFolderSync {GetVersion()} - A tool for synchronizing directories";
        return helpText;
    }
}
