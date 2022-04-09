using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GMS_Save_Installer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Game> Games { get; set; }
        public void Init()
        {
            var json = File.ReadAllText("gamelist.json");
            Games = JsonSerializer.Deserialize<ObservableCollection<Game>>(json);
            foreach (var game in Games)
            {
                game.ProgressBarVisibility = Visibility.Collapsed;
                game.GameVisibility = Visibility.Visible;
                game.GameColor = Brushes.Black;
                game.ImagePath = Path.GetFullPath(game.ImagePath);

                string path = "";
                if (game.AppData == "local")
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), game.PathTarget);
                else if (game.AppData == "roaming")
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), game.PathTarget);
                else
                {
                    MessageBox.Show($"Can't recognize AppData foler type for {game.Name}.");
                    Application.Current.Shutdown();
                }
                if (Directory.Exists(path))
                {
                    game.Installed = true;
                    game.InstallButtonCaption = "INSTALLED";
                    game.IsIntallEnabled = true;
                    game.InstallButtonColor = Brushes.Green;
                }
                else
                {
                    game.Installed = false;
                    game.InstallButtonCaption = "Install";
                    game.IsIntallEnabled = true;
                    game.InstallButtonColor = Brushes.Black;
                }
                if (!Directory.Exists(game.PathSource))
                {
                    game.GameColor = Brushes.Red;
                    game.IsIntallEnabled = false;
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Games)));
        }

        internal async Task InstallGame(Game game)
        {
            game.ProgressBarVisibility = Visibility.Visible;
            game.GameVisibility = Visibility.Collapsed;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Games)));
            await Task.Delay(1);

            if (game.Installed)
            {
                if (MessageBox.Show("Overwrite existing folder?", $"{game.Name}", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    game.ProgressBarVisibility = Visibility.Collapsed;
                    game.GameVisibility = Visibility.Visible;
                    return;
                }
            }
            var sourcePath = game.PathSource;
            string targetPath = "";
            if (game.AppData == "local")
                targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), game.PathTarget);
            else if (game.AppData == "roaming")
                targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), game.PathTarget);
            //Now Create all of the directories
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }

            game.Installed = true;
            game.ProgressBarVisibility = Visibility.Collapsed;
            game.GameVisibility = Visibility.Visible;
            game.InstallButtonColor = Brushes.Green;
            game.InstallButtonCaption = "INSTALLED";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Games)));
        }
    }
}
