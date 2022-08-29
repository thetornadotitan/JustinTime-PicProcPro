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

        public SkuHolder? holderRef = null;
        public PicHolder? picHolderRef = null;
        public TextBlock? productTitleRef = null;
        public ImagePreview? imagePreviewRef = null;
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            holderRef = FindName("SkuHolder") as SkuHolder;
            picHolderRef = FindName("PicHolder") as PicHolder;
            productTitleRef = FindName("ProductTitle") as TextBlock;
            imagePreviewRef = FindName("ImagePreview") as ImagePreview;
        }
    }
}
