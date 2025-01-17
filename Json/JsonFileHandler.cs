// Author: simon
// Created: 2024-09-20
// Copyright(c) 2024 SimonG. All Rights Reserved.

using System.Text.Json;

namespace Lib.Tools.Json;

public class JsonFileHandler<TInterface, TImplementation> where TImplementation : TInterface where TInterface : notnull
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Converters = { new AbstractConverter<TInterface, TImplementation>() }
    };

    public async Task WriteFile(TInterface value, string filePath)
    {
        await using FileStream fileStream = File.OpenWrite(filePath);
        await JsonSerializer.SerializeAsync(fileStream, value, _jsonSerializerOptions).ConfigureAwait(false);
    }

    public async Task WriteToExistingFile(TInterface value, string filePath)
    {
        await using FileStream fileStream = File.Open(filePath, FileMode.Truncate, FileAccess.Write);
        await JsonSerializer.SerializeAsync(fileStream, value, _jsonSerializerOptions).ConfigureAwait(false);
    }

    public async Task<TInterface> ReadFile(string filePath)
    {
        await using FileStream fileStream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<TInterface>(fileStream, _jsonSerializerOptions).ConfigureAwait(false) ?? throw new Exception($"Invalid {typeof(TInterface)} file.");
    }
}