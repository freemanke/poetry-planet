using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeepSeek.Core;
using DeepSeek.Core.Models;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Controls;
using PoetryPlanet.Services;
using PoetryPlanet.Views;
using MobileNavigation = PoetryPlanet.Controls.MobileNavigation;

namespace PoetryPlanet.ViewModels;

public partial class WorkViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "临江仙 · 夜归临皋";
    [ObservableProperty] private string? author = "苏轼";
    [ObservableProperty] private string? dynasty = "宋";
    [ObservableProperty] private string? pronunciation = "注音由 DeepSeek AI 实时生成，请自行鉴别结果...";
    [ObservableProperty] private string? translation = 
        "这首词作于神宗元豐五年，" +
        "即东坡黄州之贬的第三年，" +
        "写作者深秋之夜在东坡雪堂开怀畅饮，" +
        "醉後返归临皋住所的情景，" +
        "表达了词人退避社会的生活态度和希望彻底解脱的出世意念。";

    [ObservableProperty] private string? content =
        "夜饮东坡醒复醉，归来仿佛三更。" +
        "家童鼻息已雷鸣，敲门都不应，倚杖听江声。" +
        "长恨此身非我有，何时忘却营营？" +
        "夜阑风静縠纹平，小舟从此逝，江海寄余生。";

    [ObservableProperty] private string? intro =
        "这首词作于神宗元豐五年，" +
        "即东坡黄州之贬的第三年，" +
        "写作者深秋之夜在东坡雪堂开怀畅饮，" +
        "醉後返归临皋住所的情景，" +
        "表达了词人退避社会的生活态度和希望彻底解脱的出世意念。";

    [ObservableProperty] private bool isSendDisabled;
    
    [RelayCommand]
    public void Pronounce()
    {
        Task.Run(async () => { await ChatStreamAsync(); });
    }
    
    [RelayCommand]
    public void OpenMediaPlayer()
    {
        MobileNavigation.Push(new MediaPlayerView());
    }
    
    private async Task ChatStreamAsync()
    {
        IsSendDisabled = true;
        Pronunciation = "生成中...";
        Thread.Sleep(100);
        var message = "";

        var request = new ChatRequest
        {
            Messages =
            [
                Message.NewSystemMessage($"请帮我给出诗句原句和注音：{Content}"),
                Message.NewUserMessage(message)
            ],
            Model = DeepSeekModels.ChatModel
        };

        logger.LogInformation($"发送消息 {message}");
        var choices = new ChatService().client.ChatStreamAsync(request, CancellationToken.None);
        if (choices is not null)
        {
            Pronunciation = "";
            await foreach (var choice in choices)
            {
                if (choice.Delta is not null)
                {
                    var text = choice.Delta.Content;
                    Pronunciation += text;
                }
            }
        }

        IsSendDisabled = false;
    }

    
}