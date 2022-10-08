using SpaceInvaders.Core.Ports.Output;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SpaceInvaders.WPF
{
    /// <summary>
    /// Audio output for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly List<MediaPlayer> _soundPlayers = new();
        private readonly string[] _soundFiles =
        {
            "/Sounds/0.wav",
            "/Sounds/1.wav",
            "/Sounds/2.wav",
            "/Sounds/3.wav",
            "/Sounds/4.wav",
            "/Sounds/5.wav",
            "/Sounds/6.wav",
            "/Sounds/7.wav",
            "/Sounds/8.wav"
        };

        private void LoadSounds()
        {
            foreach (var file in _soundFiles)
            {
                var player = new MediaPlayer();

                player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + file));

                _soundPlayers.Add(player);
            }

            _arcadeMachine.SoundDevice.SoundChanged += SoundChanged;
            _arcadeMachine.SoundDevice.UFOEnd += UFOEnd;
        }

        private void SoundChanged(object? sender, SoundOutputChangeEventArgs e)
        {
            var sound = (int)e.Sound;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                _soundPlayers[sound].Position = TimeSpan.Zero;
                _soundPlayers[sound].Play();
            }));
        }

        private void UFOLoop(object? sender, EventArgs e)
        {
            _soundPlayers[0].Position = TimeSpan.Zero;
        }

        private void UFOEnd(object? sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _soundPlayers[0].Stop();
            }));
        }
    }
}
