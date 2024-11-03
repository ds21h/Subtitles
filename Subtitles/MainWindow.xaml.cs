using Microsoft.Win32;
using System.IO;
using System.Net.Security;
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

namespace Subtitles {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {
        private Subtitle mSubtitles;
        private List<SubtitleEntry> mSubtitleEntries;

        public MainWindow() {
            InitializeComponent();
            mSubtitles = new Subtitle();
            mSubtitleEntries = mSubtitles.xSubtitleEntries;
            lstTitles.DataContext = mSubtitleEntries;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog lDialog;
            string lFile;

            lDialog = new OpenFileDialog();
            lDialog.Filter = "NZB (*.srt)|*.srt";
            if ((bool)lDialog.ShowDialog()) {
                lFile = lDialog.FileName;
                mSubtitles.xLoadSubtitleFile(lFile);
                mSubtitleEntries = mSubtitles.xSubtitleEntries;
                lstTitles.Items.Refresh();
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog lDialog;

            lDialog = new SaveFileDialog();
            lDialog.Filter = "srt (*.srt)|*.srt";
            lDialog.FileName = mSubtitles.xTitle;
            if ((bool)lDialog.ShowDialog()) {
                mSubtitles.xWriteSubtitleFile(lDialog.FileName);
            }

        }
    }
}