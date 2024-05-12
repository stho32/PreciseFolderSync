using NUnit.Framework;
using Pfs.BL.CommandLineArguments;

namespace Pfs.BL.Tests.CommandLineArguments;

[TestFixture]
public class CommandLineArgumentsParserTests
{
    [Test]
    public void Given_Valid_Arguments_When_Parsed_Then_Returns_Options()
    {
        var args = new string[] { "--fromPath", "C:\\Source", "--toPath", "C:\\Destination" };
        var result = CommandLineArgumentsParser.ParseCommandLineArguments(args);
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Options.FromPath, Is.EqualTo("C:\\Source"));
        Assert.That(result.Options.ToPath, Is.EqualTo("C:\\Destination"));
    }

    [Test]
    public void Given_Missing_Arguments_When_Parsed_Then_Returns_Error_Code_And_Output()
    {
        var args = new string[] { "--fromPath", "C:\\Source" };
        var result = CommandLineArgumentsParser.ParseCommandLineArguments(args);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ExitCode, Is.EqualTo(1));
        Assert.That(result.Output, Is.Not.Empty);
    }

    [Test]
    public void Given_Help_Option_When_Parsed_Then_Returns_Success_Code_And_Output()
    {
        var args = new string[] { "--help" };
        var result = CommandLineArgumentsParser.ParseCommandLineArguments(args);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ExitCode, Is.EqualTo(0));
        Assert.That(result.Output, Is.Not.Empty);
    }

    [Test]
    public void Given_Version_Option_When_Parsed_Then_Returns_Success_Code_And_Output()
    {
        var args = new string[] { "--version" };
        var result = CommandLineArgumentsParser.ParseCommandLineArguments(args);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ExitCode, Is.EqualTo(0));
        Assert.That(result.Output, Is.EqualTo("PreciseFolderSync Version 1.0.0"));
    }

    [Test]
    public void Given_Invalid_Option_When_Parsed_Then_Returns_Error_Code_And_Output()
    {
        var args = new string[] { "--invalidOption" };
        var result = CommandLineArgumentsParser.ParseCommandLineArguments(args);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ExitCode, Is.EqualTo(1));
        Assert.That(result.Output, Is.Not.Empty);
    }

    [Test]
    public void Given_No_Arguments_When_Parsed_Then_Returns_Error_Code_And_Output()
    {
        var args = new string[] { };
        var result = CommandLineArgumentsParser.ParseCommandLineArguments(args);
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ExitCode, Is.EqualTo(1));
        Assert.That(result.Output, Is.Not.Empty);
    }

    [Test]
    public void Given_WhatIf_Option_When_Parsed_Then_Returns_Options_With_WhatIf_Set_To_True()
    {
        var args = new string[] { "--fromPath", "C:\\Source", "--toPath", "C:\\Destination", "--whatif" };
        var result = CommandLineArgumentsParser.ParseCommandLineArguments(args);
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Options.WhatIf, Is.True);
    }

}
