using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Services;

namespace PoetryPlanet.ViewModels;

public partial class WorkListViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<WorkListItemViewModel> works = [];
    
    [RelayCommand]
    private async Task LoadWorks()
    {
        await Task.Run(() => DoLoadWorks());
        await Task.CompletedTask;
    }

    public void DoLoadWorks()
    {
        Works.Clear();
        var workInfos = new PoetryService().GetWorkList();
        foreach (var item in workInfos)
        {
            Works.Add(new WorkListItemViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Dynasty = item.Dynasty,
                Content = item.Content
            });
        }
    }

    [RelayCommand]
    private async Task BackToFirstView()
    {
        MobileNavigation.Pop();
        await Task.CompletedTask;
    }

    [ObservableProperty] private string _poetry = "";
}
