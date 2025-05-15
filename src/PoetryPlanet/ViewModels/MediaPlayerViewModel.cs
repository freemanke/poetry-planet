using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;

namespace PoetryPlanet.ViewModels;

public partial class MediaPlayerViewModel : ViewModelBase
{

    [ObservableProperty] public string greeting = "媒体播放器";
    [ObservableProperty] public string playStatus = "点击开始播放...";

    private LibVLC? libVlc;
    private MediaPlayer? player;

    [RelayCommand]
    public void Play()
    {
        Task.Run(() => { PlaySound(); });
    }

    private void PlaySound()
    {
        try
        {
            if (player == null || libVlc == null)
            {
                libVlc = new LibVLC(enableDebugLogs: true);
                player = new MediaPlayer(libVlc);
                player.TimeChanged += (_, _) => { PlayStatus = $"{player.Time / 1000.0} / {player.Length / 1000.0}"; };
            }

            var stream = File.Open("./Assets/sample.mp3", FileMode.Open);
            using var media = new Media(libVlc, new StreamMediaInput(stream));
            player.Media = media;
            player.Play();
        }
        catch (Exception e)
        {
            PlayStatus = e.Message;
        }
    }
}