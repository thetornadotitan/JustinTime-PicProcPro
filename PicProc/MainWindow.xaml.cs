using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace PicProc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? instance;
        public string cwd = "";

        SkuHolder? holderRef = null;
        PicHolder? picHolderRef = null;
        TextBlock? productTitle = null;
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            holderRef = FindName("SkuHolder") as SkuHolder;
            picHolderRef = FindName("PicHolder") as PicHolder;
            productTitle = FindName("ProductTitle") as TextBlock;
        }

        public void SkuItemClicked(string sku, string name)
        {
            if(productTitle != null)
                productTitle.Text = name;

            if (picHolderRef != null)
                picHolderRef.BuildImageListFromSku(sku);
        }

        private void SelectFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                String path = dialog.SelectedPath;
                if (holderRef == null) return;
                holderRef.IngestPath(path);
            }
        }
    }
}
