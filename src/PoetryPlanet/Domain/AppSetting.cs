using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Media;

namespace PoetryPlanet;

public class AppSetting
{
    public static string fileName { get; } = "App.setting.json";
    public static string SQLiteFileName { get; } = "poetry-planet.sqlite";
    public static string SQLiteFilePath { get; } = Path.Combine(ConfigRootPath, SQLiteFileName);
    public static SolidColorBrush FavoriteColorBrush { get; } = new(Colors.MediumSeaGreen);
    public static SolidColorBrush UnFavoriteColorBrush { get; } = new(Colors.LightGray);
    public static string LogFilePath { get; } = Path.Combine(ConfigRootPath, "poetry.planet.log");
    public static string SQLiteUrl { get; } = "https://home.freemanke.com:60001/f/ea3a2673b9f6492a9630/?dl=1";
    public static string SampleMp3Url { get; } = "https://home.freemanke.com:60001/f/2b3bee720d434c588672/?dl=1";
    public static string ConfigRootPath => OperatingSystem.IsAndroid()
        ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    [JsonPropertyName("is_dark")] public bool IsDark { get; set; }
    [JsonPropertyName("h1_font_size")] public int H1FontSize { get; set; } = 20;
    [JsonPropertyName("body_font_size")] public int BodyFontSize { get; set; } = 16;
    [JsonPropertyName("username")] public string Username { get; set; } = "freemanke";

    [JsonPropertyName("favorite_work_ids")]
    public List<int> FavoriteWorkIds { get; set; } = [];

    public void Save()
    {
        var filePath = Path.Combine(ConfigRootPath, fileName);
        var json = JsonSerializer.Serialize(this);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"Save app setting to {filePath}, {json}");
    }

    public static AppSetting Load()
    {
        var filePath = Path.Combine(ConfigRootPath, fileName);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(new AppSetting()));
            return new AppSetting();
        }

        var json = File.ReadAllText(filePath);
        try
        {
            // 由于泛型序列化组件跨平台在 ios 上会导致反序列化的属性内容不全，
            // 需要反序列化先创建一个包含所有属性的实例后，反序列化才正常执行
            var s = new AppSetting { IsDark = true, FavoriteWorkIds = [10], BodyFontSize = 10, H1FontSize = 12 };
            var setting = JsonSerializer.Deserialize<AppSetting>(json);
            Console.WriteLine($"Load app setting from {filePath}, {json}");
            if (setting != null) return setting;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Load app setting error, {e.Message}");
        }

        return new AppSetting();
    }
}