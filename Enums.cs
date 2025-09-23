// Author: Simon.Gockner
// Created: 2025-09-23
// Copyright(c) 2025 SimonG. All Rights Reserved.

using System.Reflection;

namespace Lib.Tools;

public static class Enums
{
    public static TAttribute GetAttribute<TAttribute, TEnum>(this TEnum @enum) where TAttribute : Attribute where TEnum : Enum => 
        @enum.GetType().GetField(Enum.GetName(typeof(TEnum), @enum) ?? "")?.GetCustomAttribute<TAttribute>() ?? throw new Exception($"Can't find attribute {@enum}.");
}