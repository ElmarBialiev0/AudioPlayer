using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace AudioPlayer
{
    public partial class MainWindow : Window
    {
        private List<string> audioFiles;
        private int currentAudioIndex;
        private bool isPlaying;
        private bool isRepeatMode;
        private bool isShuffleMode;
        private CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            audioFiles = new List<string>();
            currentAudioIndex = 0;
            isPlaying = false;
            isRepeatMode = false;
            isShuffleMode = false;
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Audio Files (*.mp3, *.wav, *.m4a)|*.mp3;*.wav;*.m4a",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var selectedFiles = openFileDialog.FileNames;
                audioFiles = selectedFiles.ToList();
                currentAudioIndex = 0;
                PlayCurrentAudio();
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                PauseAudio();
            }
            else
            {
                PlayAudio();
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentAudioIndex > 0)
            {
                currentAudioIndex--;
                PlayCurrentAudio();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentAudioIndex < audioFiles.Count - 1)
            {
                currentAudioIndex++;
                PlayCurrentAudio();
            }
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            isRepeatMode = !isRepeatMode;
            SetRepeatButtonIcon();
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            isShuffleMode = !isShuffleMode;
            SetShuffleButtonIcon();

            if (isShuffleMode)
            {
                ShuffleAudioFiles();
            }
            else
            {
                SortAudioFilesByName();
            }
        }

        private async void AudioPositionSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan newPosition = TimeSpan.FromSeconds(AudioPositionSlider.Value);
                await mediaElement.Dispatcher.InvokeAsync(() =>
                {
                    mediaElement.Position = newPosition;
                });
            }
        }

        private async void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (isRepeatMode)
            {
                await mediaElement.Dispatcher.InvokeAsync(() =>
                {
                    mediaElement.Position = TimeSpan.Zero;
                    mediaElement.Play();
                });
            }
            else
            {
                NextButton_Click(null, null);
            }
        }

        private async void PlayCurrentAudio()
        {
            if (audioFiles.Any())
            {
                string currentAudioPath = audioFiles[currentAudioIndex];

                await mediaElement.Dispatcher.InvokeAsync(() =>
                {
                    mediaElement.Source = new Uri(currentAudioPath);
                    mediaElement.Play();
                    isPlaying = true;
                    SetPlayPauseButtonIcon();
                });

                UpdateAudioInfo();

                cancellationTokenSource.Cancel();
                cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;
                await UpdateAudioPosition(cancellationToken);
            }
        }

        private void PlayAudio()
        {
            mediaElement.Play();
            isPlaying = true;
            SetPlayPauseButtonIcon();
        }

        private void PauseAudio()
        {
            mediaElement.Pause();
            isPlaying = false;
            SetPlayPauseButtonIcon();
        }

        private async Task UpdateAudioPosition(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await mediaElement.Dispatcher.InvokeAsync(() =>
                {
                    if (mediaElement.NaturalDuration.HasTimeSpan)
                    {
                        TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
                        TimeSpan position = mediaElement.Position;

                        AudioPositionSlider.Minimum = 0;
                        AudioPositionSlider.Maximum = duration.TotalSeconds;
                        AudioPositionSlider.Value = position.TotalSeconds;

                        TimeSpan timeLeft = duration - position;
                        TimeLeftLabel.Content = FormatTimeSpan(timeLeft);
                        CurrentTimeLabel.Content = FormatTimeSpan(position);
                    }
                });

                await Task.Delay(500);
            }
        }

        private void UpdateAudioInfo()
        {
            string currentAudioPath = audioFiles[currentAudioIndex];
            string currentAudioFileName = Path.GetFileName(currentAudioPath);
            AudioInfoLabel.Content = currentAudioFileName;
        }

        private void SetPlayPauseButtonIcon()
        {
            string iconUri = isPlaying ? "" : "play_icon.png";
            PlayPauseButton.Icon = new BitmapImage(new Uri(iconUri, UriKind.Relative));
        }

        private void SetRepeatButtonIcon()
        {
            string iconUri = isRepeatMode ? "repeat_icon_active.png" : "repeat_icon.png";
            RepeatButton.Icon = new BitmapImage(new Uri(iconUri, UriKind.Relative));
        }

        private void SetShuffleButtonIcon()
        {
            string iconUri = isShuffleMode ? "shuffle_icon_active.png" : "shuffle_icon.png";
            ShuffleButton.Icon = new BitmapImage(new Uri(iconUri, UriKind.Relative));
        }

        private void ShuffleAudioFiles()
        {
            var random = new Random();
            int n = audioFiles.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = audioFiles[k];
                audioFiles[k] = audioFiles[n];
                audioFiles[n] = value;
            }
        }

        private void SortAudioFilesByName()
        {
            audioFiles.Sort();
        }

        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            string formattedTime = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            return formattedTime;
        }
    }
}
