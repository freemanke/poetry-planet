namespace PoetryPlanet.Web.Services;

public class SettingService
{
    public void TriggerException()
    {
        throw new Exception("由服务主动产生的异常，用于测试非页面异常产生后的恢复功能！");
    }
}