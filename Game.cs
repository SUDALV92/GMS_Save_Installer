using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace GMS_Save_Installer
{
    public class Game : INotifyPropertyChanged
    {
        private Visibility progressBarVisibility;
        private Visibility gameVisibility;
        private bool isIntallEnabled;
        private Brush installButtonColor;
        private string installButtonCaption;
        private Brush gameColor;

        public string Name { get; set; }
        public string ImagePath { get; set; }

        [JsonIgnore]
        public bool Installed { get; set; }
        public string PathTarget { get; set; }
        public string AppData { get; set; }
        public string PathSource { get; set; }

        [JsonIgnore]
        public Brush GameColor
        {
            get => gameColor;
            set
            {
                gameColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameColor)));
            }
        }

        [JsonIgnore]
        public string InstallButtonCaption
        {
            get => installButtonCaption;
            set
            {
                installButtonCaption = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InstallButtonCaption)));
            }
        }

        [JsonIgnore]
        public System.Windows.Media.Brush InstallButtonColor
        {
            get => installButtonColor;
            set
            {
                installButtonColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InstallButtonColor)));
            }
        }

        [JsonIgnore]
        public bool IsIntallEnabled
        {
            get => isIntallEnabled;
            set
            {
                isIntallEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsIntallEnabled)));
            }
        }

        [JsonIgnore]
        public Visibility GameVisibility
        {
            get => gameVisibility;
            set
            {
                gameVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameVisibility)));
            }
        }

        [JsonIgnore]
        public Visibility ProgressBarVisibility
        {
            get => progressBarVisibility;
            set
            {
                progressBarVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressBarVisibility)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
