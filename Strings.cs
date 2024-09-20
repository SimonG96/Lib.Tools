// Author: simon
// Created: 2024-09-20
// Copyright(c) 2024 SimonG. All Rights Reserved.

using System.Text.RegularExpressions;

namespace Lib.Tools;

public static partial class Strings
{
    [GeneratedRegex(@"\s+", RegexOptions.Compiled)]
    private static partial Regex WhitespaceRegex();
    
    public static string RemoveWhitespaces(this string input) => WhitespaceRegex().Replace(input, "");
}