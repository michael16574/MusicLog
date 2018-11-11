using System;
using System.Collections.Generic;
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

namespace MusicLogWPF.Views
{
    /// <summary>
    /// Interaction logic for BatchTracksWindow.xaml
    /// </summary>
    public partial class BatchTracksWindow : Window
    {
        public BatchTracksWindow()
        {
            InitializeComponent();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameEntryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public string ResponseText
        {
            get { return BatchEntryTextBox.Text; }
            set { BatchEntryTextBox.Text = value; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
