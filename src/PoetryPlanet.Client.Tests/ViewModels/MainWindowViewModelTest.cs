namespace PoetryPlanet.Client.Tests.ViewModels;

public class MainWindowViewModelTest : UnitTestBase
{
    [AvaloniaTest]
    public void ShouldHaveDefaultConstructor()
    {
        var vm = new MainWindowViewModel();
        Assert.That(vm, Is.Not.Null);
    }
    
    [AvaloniaTest]
    public void InputTextShouldTriggerBinding()
    {
        var appSetting = GetRequiredService<AppSetting>();
        appSetting.FilePath = $"/tmp/{Guid.NewGuid()}.json";
        Assert.That(File.Exists(appSetting.FilePath), Is.False);
        
        var mainWindow = GetRequiredService<MainWindow>();
        var vm = mainWindow.DataContext as MainWindowViewModel;
        Assert.That(mainWindow, Is.Not.Null);
        Assert.That(vm, Is.Not.Null);
       
        // 输入用户名后，属性绑定应该生效
        var tbxUserName = mainWindow.FindControl<TextBox>("tbxUserName");
        var username = Guid.NewGuid().ToString();
        tbxUserName.Text = username;
        Assert.That(vm.Setting.UserSetting.Username, Is.EqualTo(username));
        
        // 点击保存后，文件应该存在
        var btnSave = mainWindow.FindControl<Button>("btnSave");
        btnSave.Command!.Execute(null);
        Assert.That(File.Exists(appSetting.FilePath), Is.True);

        appSetting = AppSetting.Load(appSetting.FilePath);
        Console.WriteLine(Serializer.Serialize(appSetting));
        Assert.That(appSetting.UserSetting.Username, Is.EqualTo(username));
        File.Delete(appSetting.FilePath);
    }
}