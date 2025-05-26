using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] 
    public MineViewModel mineViewModel = App.GetRequiredService<MineViewModel>();
    
    public MainViewModel(){}
}
