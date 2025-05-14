using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.Threading;
using LibVLCSharp.Shared;

namespace PoetryPlanet.Views;

public partial class MediaPlayerView : UserControl
{
    private LibVLC MainLibVLC { get; set; }
    private MediaPlayer MainMediaPlayer { get; set; }
    private Stream? MediaStream { get; set; }
    
    public MediaPlayerView()
    {
        AvaloniaXamlLoader.Load(this);

        MainLibVLC = new LibVLC(enableDebugLogs: true);

        MainMediaPlayer = new MediaPlayer(MainLibVLC);
        MainMediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
    }
    
    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        MediaStream?.Dispose();
        MediaStream = File.Open("./Assets/sample.mp3", FileMode.Open);
        using var media = new Media(MainLibVLC, new StreamMediaInput(MediaStream));

        MainMediaPlayer.Media = media;
        MainMediaPlayer.Play();
    }

    private void MediaPlayer_TimeChanged(object? sender, MediaPlayerTimeChangedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(
            new Action(
                () =>
                {
                   // PlaybackStatus.Text = $"{MainMediaPlayer.Time / 1000.0} / {MainMediaPlayer.Length / 1000.0}";
                }
            )
        );
    }
}