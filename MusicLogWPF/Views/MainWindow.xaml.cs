using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MusicLog;

namespace MusicLogWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowListViewMenu(object sender, MouseButtonEventArgs e)
        {
            if (((ListViewItem)e.Source).IsSelected)
            {
                if (ArtistListView.SelectedItems.Count == 1)
                {
                    e.Handled = true;
                }
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var musicLog = (MainWindowViewModel)DataContext;
            musicLog.SaveMusicLog();
        }
    }
}
