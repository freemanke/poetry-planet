using System;
using System.Collections.ObjectModel;
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

public partial class CollectionViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string name = "给孩子们的事";
    [ObservableProperty] private string desc = "适合孩子诵读的古诗选本";
    [ObservableProperty] private ObservableCollection<WorkListItemViewModel> workList = [];

    public CollectionViewModel()
    {
        WorkList.Add(new WorkListItemViewModel()
        {
            Title = "题西林壁", AuthorAndDynasty = "唐·白居易", Content = "内容"
        });
    }
}