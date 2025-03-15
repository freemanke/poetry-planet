using DeepSeek.Core;
using DeepSeek.Core.Models;
using PoetryPlanet.Web.Components.Pages;

namespace PoetryPlanet.Web.Services;

public class ModelInfo
{
    public string Name { get; set; } = "";
}

public class ChatService
{
    public DeepSeekClient client { get; }
    private string apiKey = "sk-03db161e6dea4341a2eb68f36d1c859c";

    public ChatService()
    {
        client = new DeepSeekClient(apiKey);
    }

    public async Task<List<ModelInfo>> GetModelsAsync()
    {
        var models = new List<ModelInfo>();
        var response = await client.ListModelsAsync(CancellationToken.None);
        if (response is not null)
        {
            foreach (var m in response.Data)
            {
                if (m.Id != null) models.Add(new ModelInfo { Name = m.Id });
            }
        }

        return models;
    }

    public async Task<string> SendAsync(string message)
    {
        var request = new ChatRequest
        {
            Messages =
            [
                Message.NewSystemMessage("你是一个语言翻译器"),
                Message.NewUserMessage(message)
            ],
            Model = DeepSeekModels.ChatModel
        };

        var chatResponse = await client.ChatAsync(request, CancellationToken.None);
        if (chatResponse is not null)
        {
            var first = chatResponse.Choices.FirstOrDefault();
            if (first is not null && first.Message is not null)
            {
                return first.Message.Content;
            }
        }

        return "";
    }
}