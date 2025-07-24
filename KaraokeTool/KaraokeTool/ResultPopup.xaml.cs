using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KaraokeTool
{
    /// <summary>
    /// Interaktionslogik für ResultPopup.xaml
    /// </summary>
    public partial class ResultPopup : Window
    {
        private string _path = "";
        public ResultPopup(string song, string path, string name = "")
        {
            InitializeComponent();

            SongLabel.Content = song;
            NameLabel.Content = name;
            _path = path;
        }

        private void AbortBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = true;
            start.FileName = _path;
            Process.Start(start);
        }
    }
}
