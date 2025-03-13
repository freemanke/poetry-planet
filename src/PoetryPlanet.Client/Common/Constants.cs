namespace PoetryPlanet.Client;

public class Constants
{
    public const string AppTitle = "诗词星球";

    public static readonly string SettingFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PoetryPlanet/appSetting.json");
}