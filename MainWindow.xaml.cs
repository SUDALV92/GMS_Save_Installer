using System.Windows;
using System.Windows.Controls;

namespace GMS_Save_Installer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel = new MainViewModel();
            MainViewModel.Init();
        }

        private async void Install_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var game = (button.DataContext as Game);
            await MainViewModel.InstallGame(game);
        }
    }
}
