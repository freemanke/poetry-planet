using PoetryPlanet.Web;
using PoetryPlanet.Web.Services;

[assembly: ExceptionInterceptor(AttributeTargetTypes = "PoetryPlanet.Web.Services.DebugService")]

namespace PoetryPlanet.Web.Services;

public class DebugService
{
    [ExceptionInterceptor]
    public void ReturnVoid()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public string? ReturnNullableString()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public string ReturnReferenceTypeString()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public List<string> ReturnReferenceTypeListOfString()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public int ReturnScaleTypeInt()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public int? ReturnNullableInt()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public List<int> ReturnReferenceTypeListOfInt()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public float ReturnScaleTypeFloat()
    {
        throw new Exception("由服务主动产生的异常");
    }

    public Type ReturnReferenceTypeType()
    {
        throw new Exception("由服务主动产生的异常");
    }
    
    public async Task<int> ReturnScaleTypeIntAsync()
    {
        await Task.Delay(1);
        throw new Exception("由服务主动产生的异常");
    }

    public async Task ReturnVoidAsync()
    {
        await Task.Delay(1);
        throw new Exception("由服务主动产生的异常");
    }

    public async Task<int?> ReturnNullableIntAsync()
    {
        await Task.Delay(1);
        throw new Exception("由服务主动产生的异常");
    }

    public async Task<List<int>> ReturnReferenceTypeListOfIntAsync()
    {
        await Task.Delay(1);
        throw new Exception("由服务主动产生的异常");
    }
}