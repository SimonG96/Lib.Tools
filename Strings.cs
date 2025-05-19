// Author: simon
// Created: 2024-09-20
// Copyright(c) 2024 SimonG. All Rights Reserved.

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Lib.Tools;

public static partial class Strings
{
    [GeneratedRegex(@"\s+", RegexOptions.Compiled)]
    private static partial Regex WhitespaceRegex();
    
    public static string RemoveWhitespaces(this string input) => WhitespaceRegex().Replace(input, "");

    public static IEnumerable<string> ReadLines(this string input)
    {
        using StringReader stringReader = new(input);
        while (stringReader.ReadLine() is { } line)
            yield return line;
    }
    
    public static async IAsyncEnumerable<string> ReadLinesAsync(this string input, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using StringReader stringReader = new(input);
        while (await stringReader.ReadLineAsync(cancellationToken).ConfigureAwait(false) is { } line)
            yield return line;
    }

    public static bool IsNumber(this string input) => double.TryParse(input, out _);
}