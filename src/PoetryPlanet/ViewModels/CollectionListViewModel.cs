using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;

namespace PoetryPlanet.ViewModels;

public partial class CollectionListViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<CollectionListItemViewModel> list = [];
    [ObservableProperty] private string keyword = "";

    public CollectionListViewModel()
    {
        List.Add(new CollectionListItemViewModel { Name = "小学生诗词" });
        List.Add(new CollectionListItemViewModel { Name = "中国学生诗词" });
    }

    [RelayCommand]
    private void Get() => Task.Run(() => DoGet());

    [RelayCommand]
    private void Search() => Task.Run(() => DoGet());

    public void DoGet()
    {
        var infos = poetryService.GetCollectionList();
        var items = infos.Where(a =>
                (a.Name != null && a.Name.Contains(Keyword))
                || (a.Desc != null && a.Desc.Contains(Keyword)))
            .Select(a => new CollectionListItemViewModel
            {
                Id = a.Id,
                Name = a.Name ?? "",
                Kind = a.Kind ?? "",
                Desc = a.Desc ?? "",
                Title = $"{a.Name}",
            }).ToList();
        List.Clear();
        List.AddRange(items);
    }
}
