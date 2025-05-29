using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;
using Microsoft.Extensions.Logging;

namespace PoetryPlanet.ViewModels;

public partial class MediaPlayerViewModel : ViewModelBase
{
    [ObservableProperty] public string greeting = "媒体播放器";
    [ObservableProperty] public string playStatus = "点击开始播放...";
    [ObservableProperty] public bool isPlaying = false;

    private LibVLC? libVlc;
    private MediaPlayer? player;
    
    [RelayCommand]
    public void Play()
    {
        Task.Run(() => { PlaySound(); });
    }

    private void PlaySound()
    {
        IsPlaying = true;
        var filePath = Path.Combine(AppSetting.ConfigRootPath, "sample.mp3");
        if (!File.Exists(filePath))
        {
            httpService.Download(AppSetting.SampleMp3Url, filePath);
            logger.LogInformation("Download mp3 from {} to {}", AppSetting.SampleMp3Url, AppSetting.SQLiteFilePath);
        }
        logger.LogInformation("Sample mp3 file: {}", filePath);

        try
        {
            if (player == null || libVlc == null)
            {
                libVlc = new LibVLC(enableDebugLogs: true);
                player = new MediaPlayer(libVlc);
                player.TimeChanged += (_, _) => { PlayStatus = $"{player.Time / 1000.0} / {player.Length / 1000.0}"; };
            }

            using var stream = File.Open(filePath, FileMode.Open);
            using var media = new Media(libVlc, new StreamMediaInput(stream));
            player.Media = media;
            player.Play();
        }
        catch (Exception e)
        {
            PlayStatus = e.Message;
        }

        IsPlaying = false;
    }
}