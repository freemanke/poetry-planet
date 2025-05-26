using System;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoetryPlanet.ViewModels;

public partial class FavoriteWorkViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "临江仙 · 夜归临皋";
    [ObservableProperty] private string? author = "苏轼";
    [ObservableProperty] private string? dynasty = "宋";

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
    
    [ObservableProperty] private string? translation ="译文";
    
    public FavoriteWorkViewModel(){}
}