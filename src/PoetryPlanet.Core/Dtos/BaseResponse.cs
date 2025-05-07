using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

/// <summary>
/// 响应消息基类
/// </summary>
public class BaseResponse
{
    [JsonConstructor]
    public BaseResponse()
    {
        
    }
    
    /// <summary>
    /// 是否成功，
    /// 如果不成功请查看消息中的描述
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// 描述消息
    /// </summary>
    [JsonPropertyName("message")] 
    public string Message { get; set; } = "";
}

public class GetWorkResponse : BaseResponse
{
    [JsonConstructor]
    public GetWorkResponse()
    {
    }

    [JsonIgnore]
    public List<WorkInfo> Works { get; set; } = [];
}