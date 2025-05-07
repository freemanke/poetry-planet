using Newtonsoft.Json;

namespace PoetryPlanet.Dtos;

/// <summary>
/// 响应消息基类
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// 是否成功，
    /// 如果不成功请查看消息中的描述
    /// </summary>
    [JsonProperty("success")]
    public bool Success { get; set; }

    /// <summary>
    /// 描述消息
    /// </summary>
    [JsonProperty("message")] 
    public string Message { get; set; } = "";
}

public class GetWorkResponse : BaseResponse
{
    [JsonProperty("works")]
    public List<WorkInfo> Works { get; set; } = [];
}