using System;
using System.IO;
using System.Text.Json;

namespace PoetryPlanet;

public class AppSetting
{
    public static string ConfigRootPath = OperatingSystem.IsAndroid()
        ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    private static string fileName = "App.setting.json";

    public bool IsDark { get; set; }
    public int TitleSize { get; set; }
    public int BodyFontSize { get; set; }

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
            var setting = JsonSerializer.Deserialize<AppSetting>(json);
            Console.WriteLine($"Load app setting from {filePath}, {json}");
            if (setting != null) return setting;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new AppSetting();
    }
}