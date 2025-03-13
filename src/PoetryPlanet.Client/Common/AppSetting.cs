namespace PoetryPlanet.Client;

public class AppSetting
{
    public string FilePath { get; set; } = Constants.SettingFilePath;
    public UserSetting UserSetting { get; set; } = new();

    public void Save()
    {
        var json = Serializer.Serialize(this);
        var path = Path.GetDirectoryName(FilePath);
        if (string.IsNullOrEmpty(path)) return;
        try
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            File.WriteAllText(FilePath, json);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static AppSetting Load(string filePath)
    {
        if (!File.Exists(filePath)) return new AppSetting();
        try
        {
            var json = File.ReadAllText(filePath);
            var setting = JsonSerializer.Deserialize<AppSetting>(json);
            if (setting != null) return setting;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new AppSetting();
    }
}

public class UserSetting
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public int Age { get; set; } = 1;
    public string Hobby { get; set; } = "";
}