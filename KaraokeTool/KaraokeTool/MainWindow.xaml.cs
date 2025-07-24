using KaraokeTool.Data;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KaraokeTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _rand;
        private SettingsHandler _settingsHandler;

        private Dictionary<string, string> _filenamePathDict;

        private string _currentPath = "";

        public MainWindow()
        {
            InitializeComponent();
            _filenamePathDict = new Dictionary<string, string>();
            _rand = new Random();

            // Load Settings
            _settingsHandler = new SettingsHandler();

            var singers = _settingsHandler.GetUsers();
            if (!string.IsNullOrEmpty(singers))
            {
                SingerBox.Text = singers;
            }

            var path = _settingsHandler.GetPath();
            if (!string.IsNullOrEmpty(path))
            {
                LoadVideos(path);
            }
        }

        private string RandomSinger()
        {
            var singers = SingerBox.Text.Split('\n').Select(s => s.Trim()).ToList();

            var singer = singers[_rand.Next(0, singers.Count)];
            return singer;
        }

        private void ShowResultPopup(bool singer)
        {
            var song = SongBox.Items[_rand.Next(0, SongBox.Items.Count)].ToString();

            // This should not happen but we catch it anyway
            if (song == null)
            {
                return;
            }

            ResultPopup pop = new ResultPopup(song, _filenamePathDict[song], singer? RandomSinger() : "");
            pop.ShowDialog();
        }

        private void LoadVideos(string folderPath)
        {
            // Clear lists before loading
            SongBox.Items.Clear();
            _filenamePathDict.Clear();

            _currentPath = folderPath;

            if (!Directory.Exists(folderPath))
            {
                System.Windows.MessageBox.Show($"Video folder missing: {folderPath}");
                return;
            }

            // Add all mp4 files to list and fileName - fullPath mapping
            foreach (var file in Directory.EnumerateFiles(folderPath))
            {
                if (System.IO.Path.GetExtension(file).ToLower().EndsWith("mp4"))
                {
                    var fileName = System.IO.Path.GetFileName(file);
                    SongBox.Items.Add(fileName);

                    _filenamePathDict.Add(fileName, file);
                }
            }
        }

        private void FolderBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = folderBrowser.SelectedPath;

                LoadVideos(path);
            }
        }

        private void RandomSongBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowResultPopup(false);
        }

        private void RandomBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowResultPopup(true);
        }

        private void RandomSingerBtn_Click(object sender, RoutedEventArgs e)
        {
            // ToDo: Maybe add nice result Popup here as well
            System.Windows.MessageBox.Show(RandomSinger());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save settings on closing
            _settingsHandler.UpdatePath(_currentPath);
            _settingsHandler.UpdateUsers(SingerBox.Text);

            _settingsHandler.WriteSettings();
        }
    }
}