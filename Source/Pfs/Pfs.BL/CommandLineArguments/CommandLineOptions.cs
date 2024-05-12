using CommandLine;

namespace Pfs.BL.CommandLineArguments;

public class CommandLineOptions
{
    [Option('f', "fromPath", Required = true, HelpText = "Path of the source directory.")]
    public string? FromPath { get; set; }

    [Option('t', "toPath", Required = true, HelpText = "Path of the destination directory.")]
    public string? ToPath { get; set; }

    [Option('w', "whatif", Default = false, HelpText = "Simulate the operation without making any changes.")]
    public bool WhatIf { get; set; }
}