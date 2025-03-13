using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Text.Unicode;

namespace PoetryPlanet;

/// <summary>
/// 自定义序列化服务
/// </summary>
public static class Serializer
{
    /// <summary>
    /// 序列化对象，对不同语言使用对应的编码，便于阅读。
    /// </summary>
    public static string Serialize(object? obj)
    {
        return obj == null ? "" : JsonSerializer.Serialize(obj, JsonSerializerOption);
    }

    private static readonly JsonSerializerOptions JsonSerializerOption = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
    
    public static T? DeserializeIgnoreNullValue<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, IgnoreNullOptions());
    }
    
    private static JsonSerializerOptions IgnoreNullOptions() =>
        new()
        {
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    typeInfo =>
                    {
                        foreach (var propertyInfo in typeInfo.Properties)
                        {
                            var setProperty = propertyInfo.Set;
                            if (setProperty is not null)
                            {
                                propertyInfo.Set = (obj, value) =>
                                {
                                    Console.WriteLine(value);
                                    if (value != null)
                                    {
                                        setProperty(obj, value);
                                    }
                                };
                            }
                        }
                    }
                }
            }
        };
}