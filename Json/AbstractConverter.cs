// Author: Simon Gockner
// Created: 2023-10-27
// Copyright(c) 2023 SimonG. All Rights Reserved.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib.Tools.Json;

public class AbstractConverter<TInterface, TImplementation> : JsonConverter<TInterface> where TImplementation : TInterface
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(TInterface);

    public override TInterface? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => 
        JsonSerializer.Deserialize<TImplementation>(ref reader, options);

    public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options) => 
        JsonSerializer.Serialize(writer, value);
}