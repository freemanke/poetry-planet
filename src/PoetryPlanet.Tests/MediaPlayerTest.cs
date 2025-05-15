using LibVLCSharp.Shared;

namespace PoetryPlanet.Tests;

public class MediaPlayerTest
{
    [Test]
    public void Play()
    {
        var  libVlc = new LibVLC(enableDebugLogs: true);
       var   mediaPlayer = new MediaPlayer(libVlc);
        
        var mediaStream = File.Open("./Assets/sample.mp3", FileMode.Open);
        using var media = new Media(libVlc, new StreamMediaInput(mediaStream));

        mediaPlayer.Media = media;
        mediaPlayer.Play();
    }
}